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
		private readonly ApplicationDbContext applicationDbContext;
		public ICategoryRepository categoryRepository { get; private set; }
		public IProductRepository productRepository { get; private set; }
		public UnitOfWork(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
			categoryRepository = new CategoryRepository(applicationDbContext);
			productRepository = new ProductRepository(applicationDbContext);
		}
		
		public void Save()
		{
			applicationDbContext.SaveChanges();
		}
	}
}
