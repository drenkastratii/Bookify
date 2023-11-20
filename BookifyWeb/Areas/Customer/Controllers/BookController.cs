using Bookify.Data.Repository.IRepository;
using Bookify.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookifyWeb.Areas.Admin.Controllers
{
    [Area("Customer")]

    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Book> objBookList = _unitOfWork.Book.GetAll().ToList();
            return View(objBookList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book obj)
        {
            var existingBook = _unitOfWork.Book.Get(c => c.Title.ToLower() == obj.Title.ToLower());
            if (existingBook != null)
            {
                ModelState.AddModelError("Title", "The Book Already Exists");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Book.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Book created successfully";
                return RedirectToAction("Index");
            }
            return View();


        }

        
    }
}

