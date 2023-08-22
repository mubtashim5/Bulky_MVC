using BulkyWeb.DataAccess.Data;
using BulkyWeb.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyWeb.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext applicationDbContext;
		internal DbSet<T> databaseSet;
		public Repository(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
			databaseSet = applicationDbContext.Set<T>();
		}
		public void Add(T entity)
		{
			databaseSet.Add(entity);
		}

		public T Get(Expression<Func<T, bool>> filter)
		{
			IQueryable<T> values = databaseSet;
			values = values.Where(filter);
			return values.FirstOrDefault();
		}

		public IEnumerable<T> GetAll()
		{
			IQueryable<T> values = databaseSet;
			return values.ToList();
		}

		public void Remove(T entity)
		{
			databaseSet.Remove(entity);
		}

		public void RemoveRande(IEnumerable<T> entities)
		{
			databaseSet.RemoveRange(entities);
		}
	}
}
