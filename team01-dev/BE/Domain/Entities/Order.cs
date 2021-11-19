using Common.Constants;
using Domain.DTOs.Orders;
using Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order : BaseEntity

    {
        public string FullName { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public int TotalItem { get; set; }
        public string Note { get; set; }
        public Guid CouponId { get; set; }
        public string CouponName { get; set; }
        public string CouponCode { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CouponPercent { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CouponValue { get; set; }

        public Guid? CustomerId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public void Insert(Coupon coupon)
        {
            base.Insert();
            Code = CodeConstants.Code + DateTime.Now.ToString("ddMMyyyyHHmmssfff");
            TotalItem = 0;
            TotalAmount = 0;
            Status = "Mới";
            foreach (var item in OrderDetails)
            {
                item.OrderId = this.Id;
                item.Insert();
                TotalItem += item.Quantity;
                TotalAmount += item.TotalAmount;
            }

            if (coupon.IsNotNullOrEmpty())
            {
                if (coupon.HasPercent)
                {
                    CouponPercent = coupon.Value;
                    CouponValue = TotalAmount * CouponPercent / 100;
                }
                if (!coupon.HasPercent)
                {
                    CouponValue = coupon.Value;
                    CouponPercent = Math.Round((CouponValue / TotalAmount) * 100, 2);
                }

                TotalAmount -= CouponValue;
                TotalAmount = TotalAmount > 0 ? TotalAmount : 0;
            }
        }
        public override void Delete()
        {
            base.Delete();
        }

        public void Update(UpdateOrderDTO model)
        {
            base.Update();
            FullName = model.FullName;
            Address = model.Address;
            Phone = model.Phone;
            Email = model.Email;
            Status = model.Status;
            TotalAmount = model.TotalAmount;
            TotalItem = model.TotalItem;
            Note = model.Note;


            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }
    }

}
