using System;
using System.ComponentModel.DataAnnotations;
using sequor_be.Models;

namespace sequor_be{
public class ProductionInputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Order { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime ProductionDate { get; set; }

    [Required]
    [DataType(DataType.Time)]
    public string ProductionTime { get; set; }

    [Required]
    public decimal Quantity { get; set; }

    [Required]
    public string MaterialCode { get; set; }

    [Required]
    public decimal CycleTime { get; set; }


    public Production toProduction()
    {
            Production production = new Production();
            production.Order = this.Order;
            production.MaterialCode = this.MaterialCode;  
            production.CycleTime = this.CycleTime;
            production.Quantity = this.Quantity;
            production.Date = this.ProductionDate;
            production.Email = this.Email;

            return production;
    }
}

}
