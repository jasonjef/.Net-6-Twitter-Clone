using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcSocial.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
