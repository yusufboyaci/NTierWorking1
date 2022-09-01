using Core.Entity.Concrete;
using Core.Enum;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : CoreEntity
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Active(Guid id)
        {
            T activated = GetById(id);
            activated.Status = Status.Active;
            return Update(activated);
        }

        public bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return Save() > 0;
        }

        public bool Add(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return Save() > 0;
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public List<T> GetActive()
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetByDefault(Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public T GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetDefault(Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAll(Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
