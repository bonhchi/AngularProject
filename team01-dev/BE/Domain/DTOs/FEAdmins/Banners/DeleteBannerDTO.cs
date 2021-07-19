using Domain.DTOs.BaseDTOs;
using System;


namespace Domain.DTOs.Banners
{
    public class DeleteBannerDTO : BaseDeleteDTO
    {
        public Guid Id { get; set; }

    }
}
