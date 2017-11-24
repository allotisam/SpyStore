using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.ViewModels.Base;
using System.Collections.Generic;

namespace SpyStore.Service.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private IProductRepo ProductRepo { get; set; }

        public SearchController(IProductRepo productRepo)
        {
            ProductRepo = productRepo;
        }

        [HttpGet("{searchString}", Name = "SearchProducts")]
        public IEnumerable<ProductAndCategoryBase> Search(string searchString)
            => ProductRepo.Search(searchString);
    }
}
