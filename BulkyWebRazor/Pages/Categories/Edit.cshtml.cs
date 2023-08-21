using BulkyWebRazor.Data;
using BulkyWebRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories
{
	[BindProperties]
    public class EditModel : PageModel
    {
		private readonly ApplicationDbContext applicationDbContext;
		public Category category { get; set; }
		public EditModel(ApplicationDbContext applicationDbContext)
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
			if (ModelState.IsValid)
			{
				applicationDbContext.Categories.Update(category);
				applicationDbContext.SaveChanges();
				TempData["success"] = "Category updated successfully.";
				return RedirectToPage("Index");
			}
			return Page();
		}
    }
}
