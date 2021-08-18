using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.CustomerFE
{
    public class CustomerPhoneDTO
    {
        [Required]
        [StringLength(11)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4,5})$")]
        public string Phone { get; set; }
    }
}
