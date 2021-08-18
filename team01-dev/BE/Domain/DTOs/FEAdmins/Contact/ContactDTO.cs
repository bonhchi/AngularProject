using Domain.DTOs.BaseDTOs;

namespace Domain.DTOs.PageContentContact
{
    public class ContactDTO: BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
