using Microsoft.EntityFrameworkCore;
using SA.PTM.DAL.Abstract;
using SA.PTM.DAL.Context;
using SA.PTM.Entity.Abstruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.DAL.Concrete
{
    public class BaseRepo<T> : IRepo<T> where T : class, IEntity
    {
        public DbSet<T> _dbSet;
        public PersonelTakipDbContext _dbContext;
        public int _kullaniciId;

        public BaseRepo(int kullaniciId)
        {
            _dbContext = new PersonelTakipDbContext();
            _dbSet=_dbContext.Set<T>();
            _kullaniciId = kullaniciId;
        }
        public BaseRepo(PersonelTakipDbContext personelTakipDbContext)
        {
            _dbContext = new PersonelTakipDbContext();
            _dbSet = _dbContext.Set<T>();
        }
        public void Insert(T entity)
        {
            _dbSet.Add(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            SaveChanges();
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.SingleOrDefault(x => x.Id == id);
        }

    }
}
