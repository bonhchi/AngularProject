using Domain.DTOs.OrderDetails;
using System;
namespace Domain.Entities
{
    public class OrderDetail : BaseEntity

    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }



        public override void Insert()
        {
            TotalAmount = Price * Quantity;
            base.Insert();
        }
        public override void Delete()
        {
            base.Delete();
        }

        public void Update(UpdateOrderDetailDTO model)
        {
            base.Update();
            ProductId = model.ProductId;
            Price = model.Price;
            Quantity = model.Quantity;
            TotalAmount = model.TotalAmount;
            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }
    }
}
