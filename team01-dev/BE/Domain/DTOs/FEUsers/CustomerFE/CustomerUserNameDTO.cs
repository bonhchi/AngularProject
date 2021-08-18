using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.CustomerFE
{
    public class CustomerUserNameDTO
    {
        [Required]
        public string Username { get; set; }
    }
}
