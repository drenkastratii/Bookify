﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bookify.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet=db.Set<T>();
            _db.Books.Include(u => u.Category).Include(u => u.CategoryId).Include(u => u.Author).Include(u => u.AuthorId); //id???
            
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;

            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }


        public IEnumerable<T> GetAll(string? includeProperties=null)
        {
            //includat i pranojna si vlera te ndame me presje 
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var includeProp in  includeProperties.Split
                    (new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
