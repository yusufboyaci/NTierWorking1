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
using System.Transactions;

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
            return _context.Set<T>().Any(exp);
        }

        public List<T> GetActive()
        {
            return _context.Set<T>().Where(x => x.Status == Status.Active).ToList();
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetByDefault(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(exp).FirstOrDefault() ?? throw new ArgumentNullException();
        }

        public T GetById(Guid id)
        {
            return _context.Set<T>().Find(id) ?? throw new ArgumentNullException();
        }

        public List<T> GetDefault(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(exp).ToList();
        }

        public bool Remove(T entity)
        {
            entity.Status = Status.Deleted;
            return Update(entity);
        }

        public bool Remove(Guid id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    T deleted = GetById(id);
                    deleted.Status = Status.Deleted;
                    ts.Complete();
                    return Update(deleted);
                }
            }
            catch
            {

                return false;
            }
        }

        public bool RemoveAll(Expression<Func<T, bool>> exp)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                List<T> collection = GetDefault(exp);
                int count = 0;
                foreach (var item in collection)
                {
                    item.Status = Status.Deleted;
                    bool result = Update(item);
                    if (result)
                    {
                        count++;
                    }
                }
                if (collection.Count == count)
                {
                    ts.Complete();
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public bool Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                return Save() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
