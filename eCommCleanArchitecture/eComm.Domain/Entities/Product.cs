using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComm.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double? PriceTotal { get; set; }
        public double? PriceDiscount { get; set; }
        public double? PriceDiscountTotal { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageURL { get; set; }
        public DateTime CreatedDate { get; set; }

        public int CategoryID { get; set; }
        public virtual Category? Category { get; set; }
    }
}
