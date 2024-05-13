using Microsoft.AspNetCore.Mvc;
using sequor_be.Data;
using sequor_be.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add this using directive
using sequor_be.Models;
using System.ComponentModel.DataAnnotations; // Import your models namespace


namespace sequor_be.Controllers
{
    [ApiController]
    [Route("api/orders/GetProduction")]
    public class GetProduction : ControllerBase
    {
        private readonly DataContext _context;

        public GetProduction(DataContext context){
            _context = context;
         }

        [HttpGet]
        public ActionResult<IEnumerable<Production>> Get()
        {
            try
            {
                // Retrieve Orders data from the database
                var email = HttpContext.Request.Query["email"][0];
                var orders = _context.Production
                    .Include(p => p.OrderObj)
                    .Where(p => p.Email == email)
                    .Select(p => new
                    {
                        order = p.Order,
                        date = p.Date,
                        quantity = p.Quantity,
                        materialCode = p.MaterialCode,
                        cycleTime = p.CycleTime,
                    })
                    .ToList();

                return Ok(new { productions = orders });
            }
            catch (Exception ex)
            {
                // Handle exception if something goes wrong
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
