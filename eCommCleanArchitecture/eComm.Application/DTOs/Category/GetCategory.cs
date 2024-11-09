using eComm.Application.DTOs.Product;
using System.ComponentModel.DataAnnotations;

namespace eComm.Application.DTOs.Category
{
    public class GetCategory : CategoryBase
    {
        [Required]
        public int Id { get; set; }
        public ICollection<GetProduct>? Products { get; set; } 
    }
}
