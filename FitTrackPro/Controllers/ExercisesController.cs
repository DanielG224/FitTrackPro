using FitTrackPro.Data;
using FitTrackPro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FitTrackPro.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ExercisesController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

                public async Task<IActionResult> Index(string searchString)
        {
            var exercises = from e in _context.Exercises select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                exercises = exercises.Where(s => s.Name.Contains(searchString)
                                              || s.MuscleGroup.Contains(searchString));
            }

            return View(await exercises.ToListAsync());
        }

                public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(m => m.Id == id);

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

                [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

                [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exercise exercise)
        {
            ModelState.Remove("WorkoutPlanExercises");

            if (!ModelState.IsValid)
            {
                return View(exercise);
            }

            if (exercise.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(
                    _environment.WebRootPath,
                    "images");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName =
                    Guid.NewGuid().ToString() + "_" +
                    exercise.ImageFile.FileName;

                string filePath = Path.Combine(
                    uploadsFolder,
                    uniqueFileName);

                using (var fileStream = new FileStream(
                    filePath,
                    FileMode.Create))
                {
                    await exercise.ImageFile.CopyToAsync(fileStream);
                }

                exercise.ImageUrl =
                    "/images/" + uniqueFileName;
            }

            _context.Exercises.Add(exercise);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

                [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise =
                await _context.Exercises.FindAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

                [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Exercise exercise)
        {
            ModelState.Remove("WorkoutPlanExercises");

            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(exercise);
            }

            try
            {
                var existingExercise =
                    await _context.Exercises
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        e => e.Id == exercise.Id);

                if (existingExercise == null)
                {
                    return NotFound();
                }

                exercise.ImageUrl =
                    existingExercise.ImageUrl;

                if (exercise.ImageFile != null)
                {
                    string uploadsFolder =
                        Path.Combine(
                            _environment.WebRootPath,
                            "images");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName =
                        Guid.NewGuid().ToString()
                        + "_"
                        + exercise.ImageFile.FileName;

                    string filePath =
                        Path.Combine(
                            uploadsFolder,
                            uniqueFileName);

                    using (var fileStream =
                        new FileStream(
                            filePath,
                            FileMode.Create))
                    {
                        await exercise.ImageFile
                            .CopyToAsync(fileStream);
                    }

                    exercise.ImageUrl =
                        "/images/" + uniqueFileName;
                }

                _context.Update(exercise);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(exercise.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

                [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(m => m.Id == id);

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

                [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var exercise =
                await _context.Exercises.FindAsync(id);

            if (exercise != null)
            {
                _context.Exercises.Remove(exercise);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int? id)
        {
            return _context.Exercises.Any(
                e => e.Id == id);
        }
    }
}