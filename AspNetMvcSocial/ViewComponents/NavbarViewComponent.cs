using App.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcSocial.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly UserService _userService;

        public NavbarViewComponent(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _userService.Navbar());
        }
    }
}