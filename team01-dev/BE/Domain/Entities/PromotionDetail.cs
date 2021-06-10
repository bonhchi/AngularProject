using Domain.DTOs.PromotionDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PromotionDetail : BaseEntity
    {
        public Guid PromotionID { get; set; }
        public Guid ProductId { get; set; } // product price = 2,000,000 
        public decimal PriceSale { get; set; } //last price = 1,800,000 
        public decimal ValuePercent { get; set; } //value has percent from promotion = 10 
        public decimal Value { get; set; } //value from promotion = 200,000 
        public virtual Promotion Promotion { get; set; }
        public virtual Product Product { get; set; }
        public override void Insert()
        {
            base.Insert();
        }
        public override void Delete()
        {
            base.Delete();
        }

        public void Update(UpdatePromotionDetailDTO model)
        {
            base.Update();
            ProductId = model.ProductId;
            PromotionID = model.PromotionID;
            PriceSale = model.PriceSale;
            ValuePercent = model.ValuePercent;
            Value = model.Value;
            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }

    }
}
