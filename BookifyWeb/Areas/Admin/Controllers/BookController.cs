using Bookify.Data.Repository.IRepository;
using Bookify.Models.ViewModels;
using Bookify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;

namespace BookifyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Book> objBookList = _unitOfWork.Book.GetAll().ToList();
            return View(objBookList);
        }
        public IActionResult UpSert(int? id)
        {

            BookVM bookVM = new()
            {
                CategoryList = _unitOfWork.Category
                    .GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }),
                AuthorList = _unitOfWork.Author
                    .GetAll().Select(u => new SelectListItem
                    {
                        Text = u.FullName,
                        Value = u.Id.ToString()
                    }),
                Book = new Book()
            };

            if(id == null || id == 0)
            {
                //Create
                return View(bookVM);
            }
            else
            {
                //Update
                bookVM.Book = _unitOfWork.Book.Get(u => u.Id == id);    
                return View(bookVM);
            }
        }


        [HttpPost]
        public IActionResult UpSert(BookVM bookVM, IFormFile? file)
        {
            var existingBook = _unitOfWork.Book.Get(c => c.Title.ToLower() == bookVM.Book.Title.ToLower());
            if (existingBook != null)
            {
                ModelState.AddModelError("Title", "The Book Already Exists");
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string bookPath = Path.Combine(wwwRootPath, @"images\book");  

                    using(var fileStream = new FileStream(Path.Combine(bookPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    bookVM.Book.ImageUrl = @"\images\product\" + fileName;
                }



                _unitOfWork.Book.Add(bookVM.Book);
                _unitOfWork.Save();
                TempData["success"] = "Book created successfully";
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

