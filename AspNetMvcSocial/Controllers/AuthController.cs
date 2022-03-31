using App.Application.Dtos;
using App.Application.Services;
using App.Infrastructure.Data;
using AspNetMvcSocial.Data;
using AspNetMvcSocial.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcSocial.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserService _userService;

        public AuthController(AppDbContext db, UserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string email, string activationKey)
        {
            var isValid = _userService.ValidateAndActivate(email, activationKey);

            ViewBag.IsValid = isValid;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var loginTwitter = await _userService.Login(new UserDto
                {
                    UserName = loginModel.UserName,
                    Email = loginModel.UserName,
                    Password = loginModel.Password,
                    RememberMe = loginModel.RememberMe,
                });

                return Redirect(returnUrl == null ? "/" : returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre yanlış");
            return View();
        }

        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (ModelState.IsValid)
            {
                ViewBag.fpmsg = "Şifre sıfırlama kodu mailinize gönderildi";

                var userDto = await _userService.ForgotPassword(new UserDto
                {
                    Email = forgotPassword.Email,
                    UserName = forgotPassword.UserName,
                });

                return View(forgotPassword);
            }

            return View(forgotPassword);
        }

        public async Task<IActionResult> Forgot(string email, string activationKey, string pw)
        {
            ViewBag.email = email;

            ViewBag.activationKey = activationKey;

            var userChech = _db.Users.FirstOrDefault(e => e.Email == email && e.FgPwActivationKey == activationKey);

            if (userChech != null)
            {
                if (pw != null)
                {
                    userChech.Password = pw;
                    userChech.FgPwActivationKey = null;

                    _db.Users.Update(userChech);
                    _db.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction("Login", "Auth");
        }

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var userDto = await _userService.CreateUser(new UserDto
                {
                    Email = registerViewModel.EmailAdress,
                    UserName = registerViewModel.UserName,
                    Password = registerViewModel.Password,
                });
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            _userService.LogOut();

            return RedirectToAction("Index", "Auth");
        }
    }
}