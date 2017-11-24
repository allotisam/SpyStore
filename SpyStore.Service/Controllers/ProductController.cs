using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;

namespace SpyStore.Service.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IProductRepo ProductRepo { get; set; }

        public ProductController(IProductRepo productRepo)
        {
            ProductRepo = productRepo;
        }

        [HttpGet]
        public IEnumerable<ProductAndCategoryBase> Get() 
            => ProductRepo.GetAllWithCategoryName().ToList();

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = ProductRepo.GetOneWithCategoryName(id);
            if (product == null)
            {
                return NotFound();
            }

            return new ObjectResult(product);
        }

        [HttpGet("featured")]
        public IEnumerable<ProductAndCategoryBase> GetFeatured()
            => ProductRepo.GetFeaturedWithCategoryName().ToList();
    }
}
