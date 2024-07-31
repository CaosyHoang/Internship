﻿using Contract.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected HAUI_2021606204_CaoSyMinhHoangContext Context;
        protected RepositoryBase(HAUI_2021606204_CaoSyMinhHoangContext context) =>
            Context = context;
        public void Create(T entity) => Context.Set<T>().Add(entity);

        public void Delete(T entity) => Context.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ? Context.Set<T>().AsNoTracking() :
                Context.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? Context.Set<T>().Where(expression).AsNoTracking() :
                Context.Set<T>().Where(expression);

        public void Update(T entity) => Context.Set<T>().Update(entity);
    }
}
