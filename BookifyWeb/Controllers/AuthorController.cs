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

        [HttpPost]

        public IActionResult Create(Author obj)
        {
            
            if (ModelState.IsValid)
            {
                _db.Authors.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
            
        }

        public IActionResult Edit(int id)
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }
            Author? authFromDb = _db.Authors.Find(id);
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
                _db.Authors.Update(obj);
                _db.SaveChanges();
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
            Author? authFromDb = _db.Authors.Find(id);
            if (authFromDb == null)
            {
                return NotFound();
            }
            return View(authFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Author? obj = _db.Authors.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Authors.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Author deleted successfully";
            return RedirectToAction("Index");
        }


    }
}