﻿using System;
using System.Linq.Expressions;

namespace Project_OLP_Rest.Data.Interfaces
{
    public interface IGenericService<T>
    {
        T FindBy(Expression<Func<T, bool>> predicate);
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Exists(Expression<Func<T, bool>> predicate);
    }
}
