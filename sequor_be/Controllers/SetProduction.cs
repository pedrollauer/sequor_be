using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sequor_be.Data;
using sequor_be.Models;

namespace sequor_be.Controllers
{
    [ApiController]
    [Route("api/orders/SetProduction")]
    public class SetProduction : ControllerBase
    {
        private readonly DataContext _context;

        public SetProduction(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(ProductionInputModel model)
        {
            try
            {
                var users = _context.User
                            .Where(p => p.Email == model.Email)
                            .Select(p => new { p.InitialDate, p.EndDate })
                            .FirstOrDefault();


                if (users == null)
                {
                    Response response = new Response("O e-mail informado não está cadastrado.");
                    return CreatedAtAction("Post", response);
                }

                var orders = _context.Order
                             .Where(o => o.OrderCode == model.Order)
                             .Include(o => o.Product)
                             .Select(p => new { p.Quantity, p.Product.CycleTime, p.ProductCode})
                             .FirstOrDefault();

                if(orders == null) {
                    Response response = new Response("A ordem selecionada não consta no cadastro.");
                    return CreatedAtAction("Post", response);
                }

                DateTime initialDate = users.InitialDate;
                DateTime endDate = users.EndDate;

                if(model.ProductionDate < initialDate || model.ProductionDate > endDate)
                {
                    Response response = new Response("A data de apontamento deve ser validada com a data de inicio e fim cadastradas para o usuário.");
                    return CreatedAtAction("Post", response);

                }

                if(model.Quantity <= 0 || model.Quantity > orders.Quantity)
                {
                    Response response = new Response("A quantidade deve ser maior que zero e menor do que a quantidade da ordem selecionada.");
                    return CreatedAtAction("Post", response);

                }

                var material = _context.ProductMaterial
                               .Where(p => p.MaterialCode == model.MaterialCode && p.ProductCode == orders.ProductCode);
                              

                if(material.Count() == 0)
                {
                    Response response = new Response("O material deve estar na lista de materiais da ordem selecionada");
                    return CreatedAtAction("Post", response); 
                }


                string description = "";

                if (model.CycleTime < orders.CycleTime)
                {
                    description = "O apuntamento foi criado porém, tempo de ciclo informado é menor do que o tempo de ciclo do produto.";
                }

                if(description.Length==0)
                {
                    description = "Apuntamento realizado com sucesso.";
                }

                _context.Add(model.toProduction());
                _context.SaveChanges();

                Response successResponse = new Response(description);
                successResponse.Type = "S";
                successResponse.Status = 200;
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                // Handle exception if something goes wrong
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
