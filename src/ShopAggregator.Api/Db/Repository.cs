using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ShopAggregator.Api.Db
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        T GetById(object id);
        void Insert(T entity);
        void Delete(object id);
        void Delete(T toDelete);
        void Update(T toUpdate);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _ctx;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext ctx)
        {
            this._ctx = ctx;
            this._dbSet = ctx.Set<T>();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null) query = query.Where(filter);
            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public virtual T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T toDelete)
        {
            if (_ctx.Entry(toDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(toDelete);
            }

            _dbSet.Remove(toDelete);
        }

        public virtual void Update(T toUpdate)
        {
            _dbSet.Attach(toUpdate);
            _ctx.Entry(toUpdate).State = EntityState.Modified;
        }
    }
}