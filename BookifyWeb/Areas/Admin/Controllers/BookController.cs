﻿using Bookify.Data.Repository.IRepository;
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
            List<Book> objBookList = _unitOfWork.Book.GetAll(includeProperties:"Category,Author").ToList();
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
            //var bookOnUpdating = _unitOfWork.Book.Get(c => c.Id == bookVM.Book.Id);
            //if (bookVM.Book.Id == 0 || bookOnUpdating != null)
                if (bookVM.Book.Id == 0)
            {
                // Check for duplicate title before ModelState validation
                var existingBook = _unitOfWork.Book.Get(c => c.Title.ToLower() == bookVM.Book.Title.ToLower());
                if (existingBook != null)
                {
                    ModelState.AddModelError("Book.Title", "The Book Already Exists");
                }
            }
            

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string bookPath = Path.Combine(wwwRootPath, @"images\book\");

                    if(!string.IsNullOrEmpty(bookVM.Book.ImageUrl))
                    {
                        //delete the existing image
                        var oldImagePath = Path.Combine(wwwRootPath, bookVM.Book.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    bookVM.Book.ImageUrl = @"\images\book\" + fileName;
                }
                if(bookVM.Book.Id == 0)
                {
                    _unitOfWork.Book.Add(bookVM.Book);
                    _unitOfWork.Save();
                    TempData["success"] = "Book created successfully";
                }
                else
                {
                    _unitOfWork.Book.Update(bookVM.Book); 
                    _unitOfWork.Save();
                    TempData["success"] = "Book updated successfully";
                }

                
                
                return RedirectToAction("Index");
            }
            else
            {
                // Repopulate dropdown lists for categories and authors
                bookVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                bookVM.AuthorList = _unitOfWork.Author.GetAll().Select(u => new SelectListItem
                {
                    Text = u.FullName,
                    Value = u.Id.ToString()
                });

                // Return to the view with validation errors
                return View(bookVM);
            }
        }


        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Book? bookFromDb = _unitOfWork.Book.Get(c => c.Id == id);
        //    if (bookFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(bookFromDb);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    Book? obj = _unitOfWork.Book.Get(c => c.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Book.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Book deleted successfully";
        //    return RedirectToAction("Index");
        //}

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Book> objBookList = _unitOfWork.Book.GetAll(includeProperties: "Category,Author").ToList();
            return Json(new {data = objBookList});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var bookToBeDeleted = _unitOfWork.Book.Get(c => c.Id == id);
            if(bookToBeDeleted == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }
            //
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, bookToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Book.Remove(bookToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted Successfully" });

        }

        #endregion
    }
}

