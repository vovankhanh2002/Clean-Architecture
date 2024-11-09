using System.ComponentModel.DataAnnotations;

namespace eComm.Application.DTOs.Product
{
    public class ProductBase {
        [Required]
        public string ProductName { get; set; }
        public string? Description { get; set; }
        [Required]
        public double Price { get; set; }
        public double? PriceTotal { get; set; }
        public double? PriceDiscount { get; set; }
        public double? PriceDiscountTotal { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        public string? ImageURL { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public int CategoryID { get; set; }
    }
}
