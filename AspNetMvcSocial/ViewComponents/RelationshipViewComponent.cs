using App.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcSocial.ViewComponents
{
    public class RelationshipViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public RelationshipViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var realationShip = _db.Relationships
                .Include(e => e.Follwer)
                .Include(e => e.Followed)
                .ToListAsync();

            return View(realationShip);
        }
    }
}