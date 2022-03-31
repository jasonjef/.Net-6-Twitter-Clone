using App.Application.Dtos;
using App.Application.Helpers;
using App.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Services
{
    public class PostService
    {
        private readonly AppDbContext _db;
        private readonly HttpContext httpContext;

        public PostService(AppDbContext db, IHttpContextAccessor httpContext)
        {
            _db = db;
            this.httpContext = httpContext?.HttpContext;
        }

        public async Task<Post> PostDetailAsync(int id, string query)
        {
            var postDetail = await _db.Posts
                .Include(e => e.Reactions)
                .ThenInclude(e => e.User)
                .Include(e => e.User)    
                .FirstOrDefaultAsync(e => e.Id == id);

            if (query != null)
            {
                var createComment = new PostComment()
                {
                    Comment = query,
                    UserId = httpContext.GetUserId(),
                    PostId = id
                };
                _db.PostComments.Add(createComment);
                _db.SaveChanges();
            }

            return postDetail;
        }

        public async Task<List<PostComment>> GetPostCommentsAsync(int id)
        {
            return await _db.PostComments
                .Include(e => e.User)
                .Where(e => e.PostId == id)
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }

        public bool CreatePost(string query)
        {
            var createPost = new Post()
            {
                Content = query,
                UserId = httpContext.GetUserId(),
            };
            _db.Posts.Add(createPost);
            _db.SaveChanges();

            return true;
        }

        public async Task<List<Post>> GetAllPostAsync()
        {
            return await _db.Posts
                .Include(e => e.PostComments)
                .Include(e => e.Reactions)
                .Include(e => e.User)
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }

        public async Task<List<PostDto>> GetAllPostDtoAsync()
        {
            var list = await this._db.Posts.Select(e => new PostDto
            {
                Id = e.Id,
                UserName = e.User.UserName,
                Name = e.User.Name,
                ProfileImagePath = e.User.ProfileImagePath,
                Content = e.Content,
                ContentImgPath = e.ContentImgPath,
                ReactionTypeL = e.Reactions.Count > 0 ? e.Reactions[0].ReactionType : null,
                ReactionTypeR = e.Reactions.Count > 0 ? e.Reactions[1].ReactionType : null,

            }).ToListAsync();

            return list;
        }

        public bool DeletePostById(int id)
        {
            //var userId = httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            int user = httpContext.GetUserId();

            var loginVerify = _db.Posts.FirstOrDefault(e => e.UserId == user);

            if (loginVerify != null)
            {
                var deletePost = _db.Posts.FirstOrDefault(e => e.Id == id);

                _db.Posts.Remove(deletePost);

                _db.SaveChanges();

                return true;
            }

            return false;
        }

        public bool DeletePostCommentById(int id)
        {
            int user = httpContext.GetUserId();

            var loginVerify = _db.PostComments.FirstOrDefault(e => e.UserId == user);

            if (loginVerify != null)
            {
                var deleteComment = _db.PostComments.FirstOrDefault(e => e.Id == id);

                _db.PostComments.Remove(deleteComment);

                _db.SaveChanges();

                return true;
            }

            return false;
        }

        public bool CreateReaction(int postId, string reactionType)
        {
            int user = httpContext.GetUserId();

            var reaction = _db.PostReactions.FirstOrDefault(e =>
                e.PostId == postId &&
                e.UserId == user &&
                e.ReactionType == reactionType
            );

            if (reaction != null)
            {
                _db.PostReactions.Remove(reaction);
                _db.SaveChanges();
            }
            else
            {
                var newReaction = new PostReaction()
                {
                    PostId = postId,
                    UserId = user,
                    ReactionType = reactionType
                };
                _db.PostReactions.Add(newReaction);
                _db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}