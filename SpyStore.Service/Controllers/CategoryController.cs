using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;

namespace SpyStore.Service.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private ICategoryRepo CategoryRepo { get; set; }
        private IProductRepo ProductRepo { get; set; }

        public CategoryController(ICategoryRepo categoryRepo, IProductRepo productRepo)
        {
            CategoryRepo = categoryRepo;
            ProductRepo = productRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(CategoryRepo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = CategoryRepo.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Json(category);
        }

        [HttpGet("{categoryId}/products")]
        public IEnumerable<ProductAndCategoryBase> GetProductsForCategory(int categoryId)
            => ProductRepo.GetProductsForCategory(categoryId).ToList();
    }
}
