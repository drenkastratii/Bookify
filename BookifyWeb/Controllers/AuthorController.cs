using Bookify.Data.Repository.IRepository;
using Bookify.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookifyWeb.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Author> objAuthList = _unitOfWork.Author.GetAll().ToList();
            return View(objAuthList);
        }

        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]

        public IActionResult Create(Author obj)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.Author.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Author created successfully";
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
            Author? authFromDb = _unitOfWork.Author.Get(c => c.Id == id);
            if (authFromDb == null)
            {
                return NotFound();
            }
            return View(authFromDb);
            
        }

        [HttpPost]

        public IActionResult Edit(Author obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Author.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Author updated successfully";
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
            Author? authFromDb = _unitOfWork.Author.Get(c => c.Id == id);
            if (authFromDb == null)
            {
                return NotFound();
            }
            return View(authFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Author? obj = _unitOfWork.Author.Get(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Author.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Author deleted successfully";
            return RedirectToAction("Index");
        }


    }
}