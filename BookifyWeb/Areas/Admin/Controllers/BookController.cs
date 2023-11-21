using Bookify.Data.Repository.IRepository;
using Bookify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookifyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]

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
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

            IEnumerable<SelectListItem> AuthorList = _unitOfWork.Author
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.FullName,
                    Value = u.Id.ToString()
                });

            ViewBag.CategoryList = CategoryList;
            ViewBag.AuthorList = AuthorList;

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

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book? bookFromDb = _unitOfWork.Book.Get(c => c.Id == id);
            if (bookFromDb == null)
            {
                return NotFound();
            }
            return View(bookFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Book obj)
        {
            var existingBook = _unitOfWork.Book.Get(c => c.Title.ToLower() == obj.Title.ToLower());
            if (existingBook != null)
            {
                ModelState.AddModelError("Title", "The Book Already Exists");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Book.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Book updated successfully";
                return RedirectToAction("Index");
            }
            return View();


        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book? bookFromDb = _unitOfWork.Book.Get(c => c.Id == id);
            if (bookFromDb == null)
            {
                return NotFound();
            }
            return View(bookFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Book? obj = _unitOfWork.Book.Get(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Book.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Book deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

