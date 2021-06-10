using Domain.DTOs.BaseDTOs;

namespace Domain.DTOs.Categories
{
    public class CategoryDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
