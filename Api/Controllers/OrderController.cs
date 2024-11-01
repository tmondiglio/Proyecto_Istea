using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository; 
using System.Linq;
using System.Threading.Tasks;
using Api.ModelDTO;

namespace OrderControler.cs
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Items)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }


            var orderDto = new OrderDto
            {
                Id = order.Id,
                Date = order.Date,
                DeliveryDate = order.DeliveryDate,
                Customer = new CustomerDto
                {
                    Id = order.Customer.Id,
                    Name = order.Customer.Name,
                    Address = order.Customer.Address,
                    City = order.Customer.City
                },
                Items = order.Items.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Amount = i.Amount,
                    Product = new ProductDto
                    {
                        Id = i.Product.Id,
                        Description = i.Product.Description,
                        Family = i.Product.Family
                    }
                }).ToList()
            };

            return Ok(orderDto);
        }
    }
}
