using FitTrackPro.Data;
using FitTrackPro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitTrackPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WorkoutPlanExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkoutPlanExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

                public async Task<IActionResult> Create(int workoutPlanId)
        {
            ViewBag.Plans = await _context.WorkoutPlans.ToListAsync();
            ViewBag.Exercises = await _context.Exercises.ToListAsync();

            ViewBag.SelectedPlanId = workoutPlanId;

            return View();
        }

                [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int workoutPlanId, int exerciseId, int sets, int reps)
        {
            var exists = await _context.WorkoutPlanExercises
                .AnyAsync(x =>
                    x.WorkoutPlanId == workoutPlanId &&
                    x.ExerciseId == exerciseId);

            if (exists)
            {
                ModelState.AddModelError("", "To ćwiczenie już istnieje w tym planie.");

                ViewBag.Plans = await _context.WorkoutPlans.ToListAsync();
                ViewBag.Exercises = await _context.Exercises.ToListAsync();

                return View();
            }

            var item = new WorkoutPlanExercise
            {
                WorkoutPlanId = workoutPlanId,
                ExerciseId = exerciseId,
                Sets = sets,
                Reps = reps
            };

            _context.WorkoutPlanExercises.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "WorkoutPlans", new { id = workoutPlanId });
        }
    }
}