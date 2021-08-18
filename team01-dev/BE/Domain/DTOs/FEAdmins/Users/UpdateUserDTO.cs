using Domain.DTOs.Files;
using System;
using System.Collections.Generic;

namespace Domain.DTOs.Users
{
    public class UpdateUserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public Guid? CustomerId { get; set; }
        public List<FileDTO> Files { get; set; }
    }
}
