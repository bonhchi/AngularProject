using Domain.DTOs.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.InfomationWeb
{
    public class InformationWebDTO : BaseDTO
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Logo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
