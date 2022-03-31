using App.Application.Services;
using App.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcSocial.ViewComponents
{
    public class PostCommentViewComponent : ViewComponent
    {
        private readonly PostService _postService;

        public PostCommentViewComponent(PostService postService)
        {
            _postService = postService;
        }

        [Route("Post/Detail/{id}")]
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            id = ViewBag.selectPost;

            return View(await _postService.GetPostCommentsAsync(id));
        }
    }
}