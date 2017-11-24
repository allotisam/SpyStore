using Microsoft.AspNetCore.Mvc;
using SpyStore.DAL.Repos.Interfaces;

namespace SpyStore.Service.Controllers
{
    [Route("api/[controller]/{customerId}")]
    public class OrdersController : Controller
    {
        private IOrderRepo OrderRepo { get; set; }

        public OrdersController(IOrderRepo orderRepo)
        {
            OrderRepo = orderRepo;
        }

        public IActionResult GetOrderHistory(int customerId)
        {
            var orderWithTotals = OrderRepo.GetOrderHistory(customerId);

            return orderWithTotals == null ? (IActionResult)NotFound() : new ObjectResult(orderWithTotals);
        }

        [HttpGet("{orderId}", Name = "GetOrderDetails")]
        public IActionResult GetOrderForCustomer(int customerId, int orderId)
        {
            var orderWithDetails = OrderRepo.GetOneWithDetails(customerId, orderId);
            return orderWithDetails == null ? (IActionResult)NotFound() : new ObjectResult(orderWithDetails);
        }
    }
}
