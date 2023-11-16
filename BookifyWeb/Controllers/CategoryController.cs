using Microsoft.AspNetCore.Mvc;

namespace BookifyWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
