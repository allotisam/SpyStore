using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;

namespace SpyStore.Service.Controllers
{
    [Route("api/[controller]/{customerId}")]
    public class ShoppingCartController : Controller
    {
        private IShoppingCartRepo ShoppingCartRepo { get; set; }

        public ShoppingCartController(IShoppingCartRepo shoppingCartRepo)
        {
            ShoppingCartRepo = shoppingCartRepo;
        }

        [HttpGet("{productId}")]
        public CartRecordWithProductInfo GetShoppingCartRecord(int customerId, int productId)
            => ShoppingCartRepo.GetShoppingCartRecord(customerId, productId);

        [HttpGet(Name = "GetShoppingCart")]
        public IEnumerable<CartRecordWithProductInfo> GetShoppingCart(int customerId)
            => ShoppingCartRepo.GetShoppingCartRecords(customerId);

        [HttpPost]
        public IActionResult Create(int customerId, [FromBody] ShoppingCartRecord item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            item.DateCreated = DateTime.Now;
            item.CustomerId = customerId;

            ShoppingCartRepo.Add(item);

            return CreatedAtRoute("GetShoppingCart", new { controller = "ShoppingCart", customerId = customerId });
        }

        [HttpPut("{shoppingCartRecordId}")]
        public IActionResult Update(int customerId, int shoppingCartRecordId, [FromBody] ShoppingCartRecord item)
        {
            if (item == null || item.Id != shoppingCartRecordId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            item.DateCreated = DateTime.Now;
            ShoppingCartRepo.Update(item);

            // Location: http://localhost:8477/api/ShoppingCart/{customerId} (201)
            return CreatedAtRoute("GetShoppingCart", new { customerId = customerId });
        }

        [HttpDelete("{shoppingCartRecordId}/{timeStamp}")]
        public IActionResult Delete(int customerId, int shoppingCartRecordId, string timeStamp)
        {
            if (!timeStamp.StartsWith("\""))
            {
                timeStamp = $"\"{timeStamp}\"";
            }

            var ts = JsonConvert.DeserializeObject<byte[]>(timeStamp);
            ShoppingCartRepo.Delete(shoppingCartRecordId, ts);

            return NoContent();
        }

        [HttpPost("buy")]
        public IActionResult Purchase(int customerId, [FromBody] Customer customer)
        {
            if (customer == null || customer.Id != customerId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            int orderId = ShoppingCartRepo.Purchase(customerId);
            return CreatedAtRoute("GetOrderDetails", routeValues: new { customerId = customerId, orderId = orderId }, value: orderId);
        }
    }
}
