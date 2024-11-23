using MentorshipTask1.DbFile;
using MentorshipTask1.Manager;
using MentorshipTask1.Models;
using MentorshipTask1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MentorshipTask1.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthManager _authService;

        public AccountController()
        {
            _authService = new AuthManager();
        }

        // Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
                };

                using (var db = new DbContextFile())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }

                return RedirectToAction("Login");
            }

            return View(model);
        }

        // Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DbContextFile())
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == model.Username);
                    if (user == null || user.IsLockedOut)
                    {
                        return RedirectToAction("LoginFailed");
                    }

                    if (!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                    {
                        user.FailedLoginAttempts++;
                        if (user.FailedLoginAttempts >= 5)
                        {
                            user.IsLockedOut = true;
                            user.LockoutEnd = DateTime.UtcNow.AddMinutes(15); // Lock for 15 minutes
                        }

                        db.SaveChanges();
                        return RedirectToAction("LoginFailed");
                    }

                    // Reset failed login attempts on successful login
                    user.FailedLoginAttempts = 0;
                    user.IsLockedOut = false;
                    db.SaveChanges();

                    var token = _authService.GenerateJwtToken(user);
                    return Json(new { token = token });
                }
            }

            return View(model);
        }

        // Login Failed
        public ActionResult LoginFailed()
        {
            return View();
        }
    }

}