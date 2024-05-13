using Microsoft.AspNetCore.Mvc;
using sequor_be.Data;
using sequor_be.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add this using directive
using sequor_be.Models; // Import your models namespace

namespace sequor_be.Controllers
{
    [ApiController]
    [Route("api/orders/GetOrders")]
    public class GetOrders : ControllerBase
    {
        private readonly DataContext _context;

        public GetOrders(DataContext context){
            _context = context;
         }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            try
            {
                // Retrieve Orders data from the database
                var orders = _context.Order
                    .Include(o => o.Product) // Include related products
                    .ThenInclude(p => p.ProductMaterials) // Include related materials
                    .ThenInclude(p => p.Material) // Include related materials
                    .Select(o => new
                    {
                        order = o.OrderCode,
                        quantity = o.Quantity,
                        productCode = o.ProductCode,
                        productDescription = o.Product.ProductDescription,
                        image = o.Product.Image,
                        cycleTime = o.Product.CycleTime,
                        materials = o.Product.ProductMaterials
                    .Select(pm => new
                    {
                        materialCode = pm.MaterialCode,
                        materialDescription = pm.Material.MaterialDescription
                    })
                    .ToList()
                                        })
                    .ToList();

                return Ok(new { orders = orders });
            }
            catch (Exception ex)
            {
                // Handle exception if something goes wrong
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
