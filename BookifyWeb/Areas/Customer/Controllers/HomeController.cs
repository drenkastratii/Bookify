using Bookify.Data.Repository.IRepository;
using Bookify.Models;
using Bookify.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookifyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork/*, UserManager<IdentityUser> userManager*/)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            //_userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Book> bookList = _unitOfWork.Book.GetAll(includeProperties: "Category,Author");
            //var user = await _userManager.GetUserAsync(User) as ApplicationUser;
            //if (user != null)
            //{
            //    var role = await _userManager.GetRolesAsync(user);
            //    if (role.FirstOrDefault() == SD.Role_Admin)
            //    {
            //        return RedirectToPage("/Account/Manage/Index", new { area = "Identity" });
            //    }
            //}
            return View(bookList);
        }

        public IActionResult Details(int id)
        {
            Book book = _unitOfWork.Book.Get(u => u.Id == id, includeProperties: "Category,Author");
            return View(book);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
