using App.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcSocial.Controllers
{
    [Authorize(Roles = "User")]
    public class MessageController : Controller
    {
        private readonly AppDbContext _db;

        public MessageController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int mid)
        {
            ViewBag.mid = mid;

            var message = _db.Messages
                .Include(e => e.ReciverUser)
                .Include(e => e.SenderUser).Where(e => e.SenderId == mid).ToList();

            return View(message);
        }
    }
}