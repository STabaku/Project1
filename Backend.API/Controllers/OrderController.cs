using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.API.Data;
using Backend.API.Models;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL ORDERS (me Items)
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return Ok(orders);
        }

        // GET ORDER BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound(new { message = "Order not found" });

            return Ok(order);
        }

        // CREATE ORDER + ITEMS
       [HttpPost]
public async Task<IActionResult> CreateOrder([FromBody] Order order)
{
    // ================= DEBUG MODEL STATE =================
    if (!ModelState.IsValid)
    {
        var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new
            {
                Field = x.Key,
                Errors = x.Value.Errors.Select(e => e.ErrorMessage)
            });

        Console.WriteLine("===== MODEL STATE ERRORS =====");
        foreach (var error in errors)
        {
            Console.WriteLine($"Field: {error.Field}");
            foreach (var msg in error.Errors)
            {
                Console.WriteLine($"  -> {msg}");
            }
        }

        return BadRequest(ModelState);
    }

    // ================= DEBUG INPUT DATA =================
    Console.WriteLine("===== ORDER RECEIVED =====");
    Console.WriteLine($"UserId: {order.UserId}");
    Console.WriteLine($"Items Count: {order.Items?.Count}");

    if (order.Items != null)
    {
        foreach (var item in order.Items)
        {
            Console.WriteLine($"Item: {item.MedicineName}, Qty: {item.Quantity}, Price: {item.Price}");
        }
    }

    if (order.Items == null || !order.Items.Any())
        return BadRequest("Order must contain at least one item.");

    order.CreatedAt = DateTime.Now;
    order.Status = "Request Sent";

    _context.Orders.Add(order);
    await _context.SaveChangesAsync();

    Console.WriteLine("===== ORDER SAVED SUCCESSFULLY =====");

    return Ok(new
    {
        message = "Order saved successfully",
        orderId = order.Id
    });
}

        // UPDATE STATUS
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string status)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return NotFound(new { message = "Order not found" });

            order.Status = status;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order status updated" });
        }

        // ===============================
        // DELETE ORDER (Cascade fshin Items)
        // ===============================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return NotFound(new { message = "Order not found" });

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order deleted successfully" });
        }
    }
}
