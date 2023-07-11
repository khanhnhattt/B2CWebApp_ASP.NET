using System.Runtime.ConstrainedExecution;
using B2CWebApp.Models;
using B2CWebApp.Models.ViewModel;
using B2CWebApp.Services;
using B2CWebApp.Services.Impl;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2CWebApp.Controllers
{
    [Route("/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        // Login Form
        [HttpGet("login")]
        public ActionResult Login()
        {
            return View();
        }

        // POST Login
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDto)
        {

            User user = _authService.checkUserExist(loginDto);

            if (user ==null)
            {
                ViewData["msg"] = "Invalid Username or Password";
                return View("Login");
            }
            else
            {
                HttpContext.Session.SetString("u", user.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
        }

        // Get: Register Form
        [HttpGet("register")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Register
        [HttpPost("register")]
        public ActionResult Create(User user)
        {
            string msg = _authService.addUser(user);
            if (msg == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                ViewBag.msg = msg;
                return View();
            }
        }

        //// POST: AuthController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AuthController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AuthController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
