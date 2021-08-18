using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.EntityFramework
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}
