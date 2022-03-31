using App.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcSocial.ViewComponents
{
    public class TrendViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public TrendViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var usersList = await _db.Users.ToListAsync();

            var rnd = new Random();

            var remoUser = usersList.Where(e => e.UserName == User.Identity.Name).FirstOrDefault();

            usersList.Remove(remoUser);

            var rndThree = usersList.OrderBy(x => rnd.Next()).Take(3).ToList();

            return View(rndThree);
        }
    }
}