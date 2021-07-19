using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.Coupons
{
    public class DeleteCouponDTO: BaseDeleteDTO
    {
        public Guid Id { get; set; }
    }
}
