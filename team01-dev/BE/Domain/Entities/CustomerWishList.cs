using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CustomerWishList : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }


        public override void Insert()
        {
            base.Insert();
        }
        public override void Delete()
        {
            base.Delete();
        }
    }
}
