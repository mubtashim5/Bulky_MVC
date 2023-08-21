using BulkyWebRazor.Data;
using BulkyWebRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories
{
	[BindProperties]
	public class DeleteModel : PageModel
	{
		private readonly ApplicationDbContext applicationDbContext;
		public Category category { get; set; }
		public DeleteModel(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}
		public void OnGet(int? Id)
		{
			if (Id != null && Id != 0)
			{
				category = applicationDbContext.Categories.Find(Id);
			}
		}
		public IActionResult OnPost()
		{
			Category obj = applicationDbContext.Categories.Find(category.Id);
			if (obj == null)
			{
				return NotFound();
			}
			applicationDbContext.Categories.Remove(obj);
			applicationDbContext.SaveChanges();
			TempData["success"] = "Category deleted successfully.";
			return RedirectToPage("Index");
		}
	}
}
