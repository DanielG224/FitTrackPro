using FitTrackPro.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitTrackPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.ExercisesCount =
                _context.Exercises.Count();

            ViewBag.WorkoutPlansCount =
                _context.WorkoutPlans.Count();

            ViewBag.UsersCount =
                _userManager.Users.Count();

            return View();
        }
    }
}