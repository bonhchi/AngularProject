using Common.MD5;
using Domain.DTOs.Users;
using Domain.DTOs.Profiles;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Common.Enums;
using Domain.DTOs.Customer;
using Domain.DTOs.CustomerProfileFeUser;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public UserType Type { get; set; }

        public Guid? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }


        public override void Insert()
        {
            base.Insert();
        }

        public override void Delete()
        {
            base.Delete();
        }

        public void Update(UpdateUserDTO model)
        {
            base.Update();
            Username = model.Username;
            //Password = MD5Helper.ToMD5Hash(model.Password);
            Email = model.Email;
            FirstName = model.FirstName;
            LastName = model.LastName;
            ImageUrl = model.ImageUrl;
            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }

        public void Update(UserInformationDTO dto, UpdateCustomerDTO model)
        {
            base.Update(dto);
            Username = model.Username;
            //Password = MD5Helper.ToMD5Hash(model.Password);
            Email = model.Email;
            FirstName = model.FirstName;
            LastName = model.LastName;
            ImageUrl = model.ImageUrl;
            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }

        public void UpdateProfile(UpdateProfileDTO model)
        {
            base.Update();
            Email = model.Email;
            FirstName = model.FirstName;
            LastName = model.LastName;
            ImageUrl = model.ImageUrl;
            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }

        public void UpdateProfile(UserInformationDTO dto, UpdateCustomerProfileFeUserDTO model)
        {
            base.Update(dto);
            Email = model.Email;
            FirstName = model.FirstName;
            LastName = model.LastName;
            ImageUrl = model.ImageUrl;
        }

        public void ChangePassword(ChangePassworProfileDTO model)
        {
            base.Update();
            model.NewPassword = MD5Helper.ToMD5Hash(model.NewPassword);
            Password = model.NewPassword;
            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }

        public void ChangePassword(UserInformationDTO dto, ChangePasswordCustomerProfileFeUserDTO model)
        {
            base.Update(dto);
            model.NewPassword = MD5Helper.ToMD5Hash(model.NewPassword);
            Password = model.NewPassword;
        }
    }

}
