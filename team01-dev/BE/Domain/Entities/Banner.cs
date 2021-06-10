using Domain.DTOs.Banners;
using Domain.DTOs.Users;
using System;

namespace Domain.Entities
{
    public class Banner : BaseEntity
    {
        public int DisplayOrder { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }


        public override void Insert()
        {
            base.Insert();
        }
        public override void Delete()
        {
            base.Delete();
        }

        public void Update(UserInformationDTO dto, UpdateBannerDTO model)
        {
            base.Update(dto);
            Title = model.Title;
            Description = model.Description;
            DisplayOrder = model.DisplayOrder;
            ImageUrl = model.ImageUrl;
            Link = model.Link;
        }
    }
}
