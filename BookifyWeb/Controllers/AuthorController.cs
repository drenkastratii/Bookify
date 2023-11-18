using BookifyWeb.Data;
using BookifyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookifyWeb.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Author> objAuthList = _db.Authors.ToList();
            return View(objAuthList);
        }

        public IActionResult Create() 
        { 
            return View();
        }
        


    }
}