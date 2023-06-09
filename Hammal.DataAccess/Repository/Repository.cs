﻿using Hammal.DataAccess.Data;
using Hammal.DataAccess.Repository.IRepository;
using Hammal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hammal.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
		public Repository(ApplicationDbContext db)
		{
			_db = db;

			dbSet = _db.Set<T>();
		}
		public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (filter != null)
			{
				query = query.Where(filter);

			}
			if (includeProperties != null)
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return query.ToList();
		}

		public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
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
			if (includeProperties != null)
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return query.FirstOrDefault();
		}
    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
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
      if (includeProperties != null)
      {
        foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
          query = query.Include(includeProp);
        }
      }

      return await query.FirstOrDefaultAsync();
    }

    public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
      return _db.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null)
    {
      IQueryable<T> query = _db.Set<T>();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      return await query.FirstOrDefaultAsync();
    }
    public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
		}

    public void Update(T entity)
    {
			dbSet.AttachRange(entity);
			_db.Entry(entity).State= EntityState.Modified;
    }
    public virtual IQueryable<T> GetAll()
    {
      return _db.Set<T>();
    }

    public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
    {
      return _db.Set<T>().Where(predicate);
    }








  }



}