using BulkyWebRazor.Data;
using BulkyWebRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories
{
	[BindProperties]
    public class CreateModel : PageModel
    {
		private readonly ApplicationDbContext applicationDbContext;
		public Category category { get; set; }
		public CreateModel(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}
		public void OnGet()
        {
        }
		public IActionResult OnPost()
		{
			applicationDbContext.Categories.Add(category);
			applicationDbContext.SaveChanges();
			TempData["success"] = "Category created successfully.";
			return RedirectToPage("Index");
		}
    }
}
