using Bookify.Data.Repository.IRepository;
using Bookify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Data.Repository
{
    internal class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _db;
        public BookRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        public void Update(Book obj)
        {
            _db.Update(obj);
        }
    }
}
