using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UniversityManagementWebApp.Areas.Identity.Data;
using UniversityManagementWebApp.Models;

namespace UniversityManagementWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<UniversityAppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<UniversityAppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [AcceptVerbs("GET", "POST")]
        public string CheckUniqueMail(string mail)
        {
            var result = _userManager.Users.Where(u => u.Email == mail).FirstOrDefault();
            if (result != null)
            {
                return "1";
            }
            return "0";
        }

        public ActionResult VerifiedMsg()
        {
            return View();
        }

        public IActionResult Index()
        {
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