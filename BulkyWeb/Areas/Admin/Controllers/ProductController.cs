using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using BulkyWeb.Models.Models;
using BulkyWeb.DataAccess.Data;
using BulkyWeb.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using BulkyWeb.Models.ViewModels;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = unitOfWork.productRepository.GetAll(includeProperties: "Category").ToList();
            return View(products);
        }
        public IActionResult Upsert(int? Id)
        {
            IEnumerable<SelectListItem> categoryList = unitOfWork.categoryRepository.GetAll().
                Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });
            ProductVM productVM = new()
            {
                Product = new Product(),
                categories = categoryList
            };
            if (Id == null || Id == 0)
            {
                return View(productVM);
            } else
            {
                productVM.Product = unitOfWork.productRepository.Get(u => u.Id == Id);
                return View(productVM);
            }
        }
        [HttpPost] 
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                        
                    }
                    using(var fileStream = new FileStream(
                        Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    };
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                if (productVM.Product.Id == 0)
                {
					unitOfWork.productRepository.Add(productVM.Product);
					TempData["success"] = "Product created successfully.";
				} else
                {
					unitOfWork.productRepository.Update(productVM.Product);
					TempData["success"] = "Product updated successfully.";
				}  
                unitOfWork.Save();
				return RedirectToAction("Index");
			}
			else
			{
				productVM.categories = unitOfWork.categoryRepository.GetAll().
                    Select(u => new SelectListItem
				    {
					    Text = u.Name,
					    Value = u.Id.ToString()
				    });
				return View(productVM);
			}
		}
        #region API CALLs
        [HttpGet]
        public IActionResult GetAll()
        {
			List<Product> products = unitOfWork.productRepository.GetAll(includeProperties: "Category").ToList();
			return Json(new { data =  products });
		}
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
			Product? product = unitOfWork.productRepository.Get(c => c.Id == Id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting." }); 
            }
			var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}
			unitOfWork.productRepository.Remove(product);
			unitOfWork.Save();
			return Json(new { success = true, message = "Delete successful." });
		}
		#endregion
	}
}
