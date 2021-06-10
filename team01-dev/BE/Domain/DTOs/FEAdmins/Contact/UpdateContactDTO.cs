using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.Contact
{
    public class UpdateContactDTO
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}
