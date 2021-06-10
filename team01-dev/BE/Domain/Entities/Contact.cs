using Common.Constants;
using Domain.DTOs.Contact;

namespace Domain.Entities
{
    public class Contact: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public override void Insert()
        {
            base.Insert();
            Status = StatusContact.Pending;
        }
        public override void Delete()
        {
            base.Delete();
        }

        public void Update(UpdateContactDTO model)
        {
            base.Update();
            Status = model.Status;
        }

    }
}
