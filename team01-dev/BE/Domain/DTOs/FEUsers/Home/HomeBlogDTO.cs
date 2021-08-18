using Domain.DTOs.BaseDTOs;

namespace Domain.DTOs.Home
{
    public class HomeBlogDTO: BaseDTO
    {
        public string Title { get; set; }
        public string ShortDes { get; set; }
        public string ContentHTML { get; set; }
        public string ImageUrl { get; set; }

    }
}
