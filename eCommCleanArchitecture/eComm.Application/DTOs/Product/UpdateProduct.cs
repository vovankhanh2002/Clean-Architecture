using System.ComponentModel.DataAnnotations;

namespace eComm.Application.DTOs.Product
{
    public class UpdateProduct: ProductBase
    {
        [Required]
        public int Id { get; set; }
    }
}
