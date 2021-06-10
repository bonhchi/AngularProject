using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.EntityFramework
{
    public interface IEntityDeleteInfo
    {
        DateTime? DeletedDate { get; set; }
        Guid? DeletedBy { get; set; }
        bool? IsDeleted { get; set; }
    }

    public interface IEntityCreateInfo
    {
        DateTime? CreatedDate { get; set; }
        Guid? CreatedBy { get; set; }
    }

    public interface IEntityUpdateInfo
    {
        DateTime? UpdatedDate { get; set; }
        Guid? UpdatedBy { get; set; }
    }
}
