using Domain.DTOs.Blogs;
using Domain.DTOs.Categories;
using Domain.DTOs.InfomationWeb;
using System.Collections.Generic;

namespace Domain.DTOs.FEUsers.Headers
{
    public class HeaderDTO
    {
        public List<CategoryDTO> Categories { get; set; }
        public List<BlogDTO> Blogs { get; set; }
        public InformationWebDTO InformationWeb { get; set; }
    }
}
