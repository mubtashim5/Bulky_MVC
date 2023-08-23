using BulkyWeb.DataAccess.Data;
using BulkyWeb.DataAccess.Repository;
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
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext applicationDbContext;
		public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}

		public void Update(Category category)
		{
			applicationDbContext.Categories.Update(category);
		}
	}
}
