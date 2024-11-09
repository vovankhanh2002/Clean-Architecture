using System.ComponentModel.DataAnnotations;

namespace eComm.Application.DTOs.Category
{
    public class CategoryBase
    {
        [Required]
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public DateTime DateTimeCreate { get; set; }
        public DateTime DateTimeClose { get; set; }
        public bool IsActive { get; set; } 
        public bool IsDeleted { get; set; } = false;
    }
}
