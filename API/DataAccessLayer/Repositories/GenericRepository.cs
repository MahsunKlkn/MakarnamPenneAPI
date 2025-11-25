using DataAccessLayer.Abstarct;
using DataAccessLayer.Concrete.EntityRepository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        protected readonly Context _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Delete(T t)
        {
            _dbSet.Remove(t);
            _context.SaveChanges();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetListAll()
        {
            return _dbSet.ToList();
        }

        public void Insert(T t)
        {
            _dbSet.Add(t);
            _context.SaveChanges();
        }

        public void Update(T t)
        {
            _dbSet.Update(t);
            _context.SaveChanges();
        }
    }
}
