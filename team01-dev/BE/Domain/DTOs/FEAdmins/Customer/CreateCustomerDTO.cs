using Domain.DTOs.BaseDTOs;
using Domain.DTOs.Files;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Customer
{
    public class CreateCustomerDTO : BaseCreateDTO
    {
        [Required]
        [StringLength(35)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(35)]
        public string LastName { get; set; }
        [StringLength(90)]
        public string Address { get; set; }
        [Required]
        [StringLength(90)]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")]
        public string Email { get; set; }
        [StringLength(11)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        public string Phone { get; set; }

        [Required]
        public string Username { get; set; }
        public string Password { get; set; }

        public string ImageUrl { get; set; }
        public List<FileDTO> Files { get; set; }
    }
}
