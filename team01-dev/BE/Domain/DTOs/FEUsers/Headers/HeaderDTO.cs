using Domain.DTOs.Blogs;
using Domain.DTOs.Categories;
using Domain.DTOs.InfomationWeb;
using System.Collections.Generic;


namespace Domain.DTOs.FEUsers.Headers
{
    public class HeaderDTO
    {
        public List<CategoryDTO> categories { get; set; }
        public List<BlogDTO> blogs { get; set; }
        public InformationWebDTO informationWeb { get; set; }
    }
}
