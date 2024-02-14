using System;
using System.ComponentModel.DataAnnotations;

namespace invenio.Models.Dtos
{
    public record SaleOrderDto
    {
        [Required]
        public Guid ProductId { get; set; }
        
        [Required]
        public int Amount { get; set; }
        
        [Required]
        public decimal OrderPrice { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
    }
    public record CreateSaleOrderDto
    {
        [Required]
        public Guid ProductId { get; set; }
        
        [Required]
        public int Amount { get; set; }
        
        [Required]
        public decimal OrderPrice { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
    }

    public record UpdateSaleOrderDto
    {
        public Guid? ProductId { get; set; }
        public int? Amount { get; set; }
        public decimal? OrderPrice { get; set; }
        public DateTime? Date { get; set; }
    }
}
