using BulkyWeb.DataAccess.Data;
using BulkyWeb.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyWeb.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private ApplicationDbContext applicationDbContext;
		public ICategoryRepository categoryRepository { get; private set; }
		public UnitOfWork(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
			categoryRepository = new CategoryRepository(applicationDbContext);
		}
		
		public void Save()
		{
			applicationDbContext.SaveChanges();
		}
	}
}
