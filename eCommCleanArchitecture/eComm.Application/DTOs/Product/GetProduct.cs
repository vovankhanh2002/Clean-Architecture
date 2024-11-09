using eComm.Application.DTOs.Category;
using System.ComponentModel.DataAnnotations;
namespace eComm.Application.DTOs.Product
{
    public class GetProduct : ProductBase
    {
        [Required]
        public int Id { get; set; }
        public GetCategory? Category { get; set; }
    }
}
