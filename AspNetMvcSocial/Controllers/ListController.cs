using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcSocial.Controllers
{
    [Authorize(Roles = "User")]
    public class ListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
