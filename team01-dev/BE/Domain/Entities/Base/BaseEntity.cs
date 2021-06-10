using Domain.DTOs.Users;
using Infrastructure.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BaseEntity : IObjectState
    {
        [Key, Required]
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedByName { get; set; }
        public Guid CreatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public Guid UpdatedBy { get; set; }
        public string DeletedByName { get; set; }
        public Guid DeletedBy { get; set; }
        public DateTime CreateByDate { get; set; }
        public DateTime UpdateByDate { get; set; }
        public DateTime DeleteByDate { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; }

        public virtual void Insert()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            IsDeleted = false;
            CreateByDate = DateTime.Now;
            UpdateByDate = DateTime.Now;
            DeleteByDate = new DateTime();
            CreatedByName = "admin";
            UpdatedByName = "admin";
            ObjectState = Infrastructure.EntityFramework.ObjectState.Added;
        }
        public virtual void Delete()
        {
            IsDeleted = true;
            DeleteByDate = DateTime.Now;
            DeletedByName = "admin";
            ObjectState = Infrastructure.EntityFramework.ObjectState.Deleted;
        }

        public virtual void Update()
        {
            UpdateByDate = DateTime.Now;
            IsActive = true;
            UpdatedByName = "admin";
            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }

        public virtual void Insert(UserInformationDTO dto)
        {
            Id = Guid.NewGuid();
            IsActive = true;
            IsDeleted = false;
            CreateByDate = DateTime.Now;
            UpdateByDate = DateTime.Now;
            DeleteByDate = new DateTime();
            CreatedBy = dto.UserId;
            CreatedByName = dto.Username;
            UpdatedBy = dto.UserId;
            UpdatedByName = dto.Username;
            ObjectState = Infrastructure.EntityFramework.ObjectState.Added;
        }

        public virtual void Update(UserInformationDTO dto)
        {
            UpdateByDate = DateTime.Now;
            IsActive = true;
            UpdatedBy = dto.UserId;
            UpdatedByName = dto.Username;
            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }

        public virtual void Delete(UserInformationDTO dto)
        {
            UpdateByDate = DateTime.Now;
            IsActive = true;
            DeletedBy = dto.UserId;
            DeletedByName = dto.Username;
            ObjectState = Infrastructure.EntityFramework.ObjectState.Modified;
        }
    }
}
