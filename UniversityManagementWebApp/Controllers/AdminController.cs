using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementWebApp.Areas.Identity.Data;

namespace UniversityManagementWebApp.Controllers
{

    [Authorize(Policy = "AdminApprovalPage")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<UniversityAppUser> _userManager;
        public AdminController(ILogger<AdminController> logger, UserManager<UniversityAppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            var data = _userManager.Users.Where(u => u.Status == "NotApproved").ToList();
            return View("Index", data);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveAction(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Status = "Approved   ";
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                return RedirectToAction("ErrorPage");

            }
            return RedirectToAction("ErrorPage");
        }

     

        [HttpPost]
        public async Task<IActionResult> RejectAction(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Status = "Rejected   ";
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                return RedirectToAction("ErrorPage");

            }
            return RedirectToAction("ErrorPage");

            //var user = await _userManager.FindByIdAsync(id);
            //if (user != null)
            //{
            //    IdentityResult result = await _userManager.DeleteAsync(user);
            //    if (result.Succeeded)
            //        return RedirectToAction("Index");
            //    return RedirectToAction("ErrorPage");
            //}
            //return RedirectToAction("ErrorPage");

        }

        public ActionResult ErrorPage()
        {
            return View();
        }


    }
}
