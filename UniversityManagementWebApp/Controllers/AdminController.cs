using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementWebApp.Areas.Identity.Data;

namespace UniversityManagementWebApp.Controllers
{

    [Authorize(Policy = "AdminApprovalPage")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<UniversityAppUser> _userManager;
        private IPasswordHasher<UniversityAppUser> _passwordHasher;
        public AdminController(ILogger<AdminController> logger, UserManager<UniversityAppUser> userManager, IPasswordHasher<UniversityAppUser> passwordHasher)
        {
            _logger = logger;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<IActionResult> ApproveAction(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = user.Email;
                user.PanNumber = user.PanNumber.ToString();
                user.PasswordHash = user.PasswordHash;
                user.Status = "Approved   ";
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RejectAction(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        // GET: AdminController
        public ActionResult Index()
        {
            var data = _userManager.Users.Where(u=>u.Status=="NotApproved").ToList();
            return View("index",data);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
