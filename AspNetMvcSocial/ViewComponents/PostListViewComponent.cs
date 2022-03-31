using App.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcSocial.ViewComponents
{
    public class PostListViewComponent : ViewComponent
    {
        private readonly PostService _postService;

        public PostListViewComponent(PostService postService)
        {
            _postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _postService.GetAllPostAsync());
        }
    }
}