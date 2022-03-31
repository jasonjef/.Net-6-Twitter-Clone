using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcSocial.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
