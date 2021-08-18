using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.Coupons
{
    public class UpdateCouponDTO: BaseUpdateDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool HasPercent { get; set; }
        public decimal Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
