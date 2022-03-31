using App.Application.Dtos;
using App.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<List<PostDto>> GetAllPostAsync()
        {
            return await _postService.GetAllPostDtoAsync();
        }
    }
}