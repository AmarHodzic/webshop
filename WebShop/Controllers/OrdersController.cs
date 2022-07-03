#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop;
using WebShop.Data;

namespace WebShop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        //GET: api/Orders/Products/1
        [HttpGet("Products/{OrderId}")]
        public async Task<ActionResult<ProductOrders>> GetProductsOfOrder(int OrderId)
        {
            //var listOforders = _context.ProductOrders.Select(r=>r.OrderId);
            var orderProducts = _context.ProductOrders.Where(r => r.OrderId == OrderId);

            return Ok(orderProducts);
        }

        //GET: api/Orders/search
        [HttpGet("search/{keyword}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByKeyword(string keyword)
        {
            var keywordOrders = _context.Orders.Where(r => r.adress.Contains(keyword));

            return Ok(keywordOrders);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{orderState}")]
        public async Task<IActionResult> PutOrder([FromRoute]int id, [FromRoute] int orderState, [FromBody]Order order)
        {
            if (id != order.id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                if(orderState == 0)
                {
                    Order edited = _context.Orders.FirstOrDefault(c => c.id == id);
                    edited.state = "canceled";
                }
                else if(orderState == 1)
                {
                    Order edited = _context.Orders.FirstOrDefault(c => c.id == id);
                    edited.state = "shipping...";
                }
                else if (orderState == 2)
                {
                    Order edited = _context.Orders.FirstOrDefault(c => c.id == id);
                    edited.state = "delivered";
                }
                //edited.desc = category.desc;
                //edited.title = category.title;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{userId}")]
        public async Task<ActionResult<Order>> PostOrder(int userId, OrderDTO orderDTO)
        {
            User u = _context.Users.FirstOrDefault(i=>i.id == userId);

            Order order = new Order();

            Order test = _context.Orders.OrderByDescending(u => u.id).FirstOrDefault();
            int id = 0;
            if (test!= null)
                id = test.id;
            
            order.price = orderDTO.price;
            order.state = orderDTO.state;
            order.adress = orderDTO.adress;
            order.quantities = orderDTO.quantities;
            order.id = id+1;
            List<ProductOrders> productOrders = new List<ProductOrders>();
            //order.products = new List<Product>();
            for(int i = 0; i < orderDTO.products.Length; i++)
            {
                ProductOrders productOrder = new ProductOrders();
                productOrder.ProductId = orderDTO.products[i];
                productOrder.OrderId = order.id;    
                productOrders.Add(productOrder);
            }
            if(u.orders == null)
            {
                u.orders = new List<Order>();
            }
            _context.Orders.Add(order);
            u.orders.Add(order);
            _context.ProductOrders.AddRange(productOrders);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.id == id);
        }
    }
}
