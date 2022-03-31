using App.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcSocial.ViewComponents
{
    public class PostFormCommentViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public PostFormCommentViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _db.Users.FirstOrDefaultAsync(e => e.UserName == User.Identity.Name);

            return View(user);
        }
    }
}