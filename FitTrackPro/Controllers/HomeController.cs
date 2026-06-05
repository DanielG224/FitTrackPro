using FitTrackPro.Data;
using FitTrackPro.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace FitTrackPro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var welcomeText = _context.CmsContents
                .FirstOrDefault(c => c.SectionKey == "Home_Welcome")?.ContentValue
                ?? "Witaj w FitTrackPro! Odkryj najlepsze plany treningowe.";

            ViewBag.WelcomeText = welcomeText;

            var exercises = _context.Exercises.ToList();

            if (exercises.Any())
            {
                int index = DateTime.Today.DayOfYear % exercises.Count;
                ViewBag.ExerciseOfTheDay = exercises[index];
            }

            ViewBag.ExerciseCount = _context.Exercises.Count();
            ViewBag.WorkoutPlanCount = _context.WorkoutPlans.Count();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}