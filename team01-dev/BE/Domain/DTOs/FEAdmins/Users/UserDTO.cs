using Domain.DTOs.BaseDTOs;
using System;

namespace Domain.DTOs.Users
{
    public class UserDTO : BaseDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
