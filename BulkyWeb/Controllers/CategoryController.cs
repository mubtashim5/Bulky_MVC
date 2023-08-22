using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Bulky.Models.Models;
using Bulky.DataAccess.Data;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = applicationDbContext.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
/*            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Category name and Display Order cannot be exactly the same!");
            }*/
            if (ModelState.IsValid)
            {
				applicationDbContext.Categories.Add(obj);
				applicationDbContext.SaveChanges();
				TempData["success"] = "Category created successfully.";
				return RedirectToAction("Index");
			}
            return View();
        }
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Category? category = applicationDbContext.Categories.FirstOrDefault(c => c.Id == Id);
            Category? category2 = applicationDbContext.Categories.Find(Id);
			Category? category3 = applicationDbContext.Categories.Where(c => c.Id == Id).FirstOrDefault();
			if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				applicationDbContext.Categories.Update(obj);
				applicationDbContext.SaveChanges();
				TempData["success"] = "Category updated successfully.";
				return RedirectToAction("Index");
			}
			return View();
		}
		public IActionResult Delete(int? Id)
		{
			if (Id == null || Id == 0)
			{
				return NotFound();
			}
			Category? category = applicationDbContext.Categories.FirstOrDefault(c => c.Id == Id);
			Category? category2 = applicationDbContext.Categories.Find(Id);
			Category? category3 = applicationDbContext.Categories.Where(c => c.Id == Id).FirstOrDefault();
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}
		[HttpPost]
		[ActionName("Delete")]
		public IActionResult DeletePOST(int? Id)
		{
			Category? category = applicationDbContext.Categories.Find(Id);
			if (category == null)
			{
				return NotFound();
			}
			applicationDbContext.Categories.Remove(category);
			applicationDbContext.SaveChanges();
			TempData["success"] = "Category deleted successfully.";
			return RedirectToAction("Index");
		}
	}
}
