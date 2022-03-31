using App.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcSocial.ViewComponents
{
    public class MessageViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public MessageViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var messageList = _db.Messages
                .Include(e => e.SenderUser)
                .Include(e => e.ReciverUser);

            var ownerMessage = messageList.Where(e => e.ReciverUser.UserName == User.Identity.Name).ToList();

            return View(ownerMessage);
        }
    }
}