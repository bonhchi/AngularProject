using Domain.DTOs.BaseDTOs;


namespace Domain.DTOs.InfomationWeb
{
    public class UpdateInformationWebDTO : BaseUpdateDTO
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Logo { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
