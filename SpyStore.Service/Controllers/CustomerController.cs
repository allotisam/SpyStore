using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;
using System.Collections.Generic;

namespace SpyStore.Service.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private ICustomerRepo CustomerRepo { get; set; }

        public CustomerController(ICustomerRepo customerRepo)
        {
            CustomerRepo = customerRepo;
        }

        [HttpGet]
        public IEnumerable<Customer> Get() => CustomerRepo.GetAll();

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = CustomerRepo.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return new ObjectResult(customer);
        }
    }
}
