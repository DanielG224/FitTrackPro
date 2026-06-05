using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitTrackPro.Models;
using FitTrackPro.Data;

namespace FitTrackPro.Controllers
{
    public class WorkoutPlansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkoutPlansController(ApplicationDbContext context)
        {
            _context = context;
        }

                public async Task<IActionResult> Index()
        {
            return View(await _context.WorkoutPlans.ToListAsync());
        }

                public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var workoutPlan = await _context.WorkoutPlans
                .Include(p => p.WorkoutPlanExercises)
                    .ThenInclude(wpe => wpe.Exercise)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (workoutPlan == null)
                return NotFound();

            return View(workoutPlan);
        }

                [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

                [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkoutPlan workoutPlan)
        {
            if (!ModelState.IsValid)
                return View(workoutPlan);

            _context.WorkoutPlans.Add(workoutPlan);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

                [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var workoutPlan = await _context.WorkoutPlans.FindAsync(id);

            if (workoutPlan == null)
                return NotFound();

            return View(workoutPlan);
        }

                [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkoutPlan workoutPlan)
        {
            if (id != workoutPlan.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(workoutPlan);

            try
            {
                _context.Update(workoutPlan);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutPlanExists(workoutPlan.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

                [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var workoutPlan = await _context.WorkoutPlans
                .FirstOrDefaultAsync(m => m.Id == id);

            if (workoutPlan == null)
                return NotFound();

            return View(workoutPlan);
        }

                [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workoutPlan = await _context.WorkoutPlans.FindAsync(id);

            if (workoutPlan != null)
            {
                _context.WorkoutPlans.Remove(workoutPlan);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutPlanExists(int id)
        {
            return _context.WorkoutPlans.Any(e => e.Id == id);
        }
    }
}