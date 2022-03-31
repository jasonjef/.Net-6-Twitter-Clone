using App.Application.Services;
using App.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcSocial.Controllers
{
    [Authorize(Roles = "User")]
    public class ProfileController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserService _userService;

        public ProfileController(UserService userService)
        {

            _userService = userService;
        }

        public async Task<IActionResult> Index(string name)
        {
            ViewBag.Name = name;

            return View(await _userService.Profile(name));
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}