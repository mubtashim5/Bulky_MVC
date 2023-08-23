using BulkyWeb.DataAccess.Data;
using BulkyWeb.DataAccess.Repository.IRepository;
using BulkyWeb.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyWeb.DataAccess.Repository
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly ApplicationDbContext dbContext;
		public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			this.dbContext = dbContext;
		}
		public void Update(Product product)
		{
			/*dbContext.Products.Update(product);*/
			Product product1 = dbContext.Products.FirstOrDefault(u => u.Id == product.Id);
			if (product1 != null)
			{
				product1.Title = product.Title;
				product1.Description = product.Description;
				product1.ISBN = product.ISBN;
				product1.Author = product.Author;
				product1.CategoryId = product.CategoryId;
				product1.ListPrice = product.ListPrice;
				product1.Price = product.Price;
				product1.Price50 = product.Price50;
				product1.Price100 = product.Price100;
				if (product.ImageUrl != null)
				{
					product1.ImageUrl = product.ImageUrl;
				}
			}
		}
		}
}
