﻿using Microsoft.EntityFrameworkCore;
using Project_OLP_Rest.Data.Services;
using Project_OLP_Rest.Domain;
using System;
using System.Linq.Expressions;

namespace Project_OLP_Rest.Data.Interfaces
{
    public abstract class GenericService<T> : Service, IGenericService<T> where T : Entity
    {
        protected DbSet<T> _entities;

        protected GenericService(OLP_Context context) : base(context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual T Create(T entity)
        {
            T addedEntity = _entities.Add(entity).Entity;
            _context.SaveChanges();
            return addedEntity;
        }

        public virtual void Delete(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public virtual T FindBy(Expression<Func<T, bool>> predicate)
        {
            return _entities.SingleAsync(predicate).Result;
        }

        public virtual void Update(T entity)
        {
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _entities.AnyAsync(predicate).Result;
        }
    }
}
