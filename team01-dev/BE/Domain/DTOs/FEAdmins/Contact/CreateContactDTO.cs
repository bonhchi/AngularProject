using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.Contact
{
    public class CreateContactDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
