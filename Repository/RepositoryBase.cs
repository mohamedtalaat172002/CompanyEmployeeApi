﻿using Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T :class 
    {
        protected ApplicationDbContext _context;
            public RepositoryBase(ApplicationDbContext context )
        {
            _context = context;
        }

        public void Create(T entity)
        => _context.Set<T>().Add( entity );


        public void Delete(T entity)
        => _context.Set<T>().Remove(entity);

        public void Update(T entity)
         => _context.Set<T>().Update( entity );




        public IQueryable<T> FindAll(bool TrackChanges)
        =>
            !TrackChanges ?
            _context.Set<T>()
            .AsNoTracking() : _context.Set<T>();



        public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool TraackChanges)
        => !TraackChanges ?
            _context.Set<T>().Where(expression)
            .AsNoTracking():_context.Set<T>();

       
                
        
    }
}
