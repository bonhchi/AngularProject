using System;

namespace Domain.DTOs.Contact
{
    public class UpdateContactDTO
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}
