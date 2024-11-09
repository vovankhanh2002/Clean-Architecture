using System.ComponentModel.DataAnnotations;

namespace eComm.Application.DTOs.Category
{
    public class UpdateCategory:CategoryBase
    {
        [Required]
        public int Id { get; set; }
    }
}
