using App.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcSocial.ViewComponents
{
    public class MessageBoxViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public MessageBoxViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            id = ViewBag.mid;

            var message = await _db.Messages
                .Include(e => e.ReciverUser)
                .Include(e => e.SenderUser).Where(e => e.SenderId == id).ToListAsync();

            var reciversender = await _db.Messages
                .Include(e => e.ReciverUser)
                .Include(e => e.SenderUser).Where(e => e.SenderUser.Name == User.Identity.Name).ToListAsync();

            //var reciversender = message.Where(e => e.SenderUser.UserName == User.Identity.Name).ToList();

            var asd = reciversender.Where(e => e.SenderId == id).ToList();

            ViewData["reciversender"] = reciversender;

            return View(message);
        }
    }
}