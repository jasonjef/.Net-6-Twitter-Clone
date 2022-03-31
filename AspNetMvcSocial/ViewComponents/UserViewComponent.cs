using App.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcSocial.ViewComponents
{
    public class UserViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public UserViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userPostList = await _db.Posts
                .Include(e => e.User)
                .ToListAsync();

            var ownerUserPostList = userPostList.Where(e => e.User.UserName == ViewBag.Name).ToList();

            return View(ownerUserPostList);
        }
    }
}