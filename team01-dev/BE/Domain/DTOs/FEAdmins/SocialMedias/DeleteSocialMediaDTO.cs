using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.SocialMedias
{
    public class DeleteSocialMediaDTO: BaseDeleteDTO
    {
        public Guid Id { get; set; }
    }
}
