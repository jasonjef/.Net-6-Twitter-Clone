using App.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcSocial.Controllers
{
    [Authorize(Roles = "User")]
    public class ExploreController : Controller
    {
        private readonly PostService _postService;

        public ExploreController(PostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _postService.GetAllPostAsync());
        }
    }
}