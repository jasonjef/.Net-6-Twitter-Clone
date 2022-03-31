using App.Application.Services;
using App.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AspNetMvcSocial.Controllers
{
    [Authorize(Roles = "User")]
    public class PostController : Controller
    {
        private readonly AppDbContext _db;
        private readonly PostService _postService;
        private readonly HttpContext httpContext;

        public PostController(AppDbContext db, IHttpContextAccessor httpContext, PostService postService)
        {
            _db = db;
            _postService = postService;
            this.httpContext = httpContext?.HttpContext;
        }

        public async Task<IActionResult> Detail(int id, string query)
        {
            ViewBag.selectPost = id;

            return View(await _postService.PostDetailAsync(id, query));
        }

        public IActionResult CommentDetail(int id, string query)
        {
            ViewBag.selectPost = id;

            if (query != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                int usersId = int.Parse(userId);

                var createPost = new PostComment()
                {
                    Comment = query,
                    UserId = usersId,
                    PostId = id,
                    CreatedAt = DateTime.Now,
                };

                _db.Add(createPost);
                _db.SaveChanges();
            }

            var post = _db.Posts
                .Include(e => e.User)
                .FirstOrDefault(e => e.Id == id);

            return View(post);
        }

        [HttpPost]
        public IActionResult Create(string query)
        {
            var createPost = _postService.CreatePost(query);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id)
        {
            var isDeleted = _postService.DeletePostById(id);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult CommentDelete(int id)
        {
            var isDeleted =  _postService.DeletePostCommentById(id);

            return Redirect("/");
        }

        [HttpPost]
        public bool Reaction(int postId, string reactionType)
        {
            var isReaction = _postService.CreateReaction(postId, reactionType);

            return true;
        }
    }
}