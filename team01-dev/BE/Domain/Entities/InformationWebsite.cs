using Domain.DTOs.InfomationWeb;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class InformationWebsite : BaseEntity
    {
        public string Address { get; set; } 
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public void Update(UpdateInformationWebDTO model)
        {
            base.Update();
            Address = model.Address;
            Phone = model.Phone;
            Email = model.Email;
            Fax = model.Fax;
            Logo = model.Logo;
            Title = model.Title;
            Description = model.Description;
            UpdateByDate = DateTime.Now;
            UpdatedByName = model.UpdatedByName;
            UpdatedBy = model.UpdatedBy;
            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }
    }
}
