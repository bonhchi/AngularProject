using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.User
{
    public class UserDataReturnDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string Token { get; set; }
    }
}
