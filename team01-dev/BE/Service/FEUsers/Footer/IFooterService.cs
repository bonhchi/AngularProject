using Common.Http;
using Domain.DTOs.FEUsers.Footers;
using System.Threading.Tasks;

namespace Service.Footer
{
    public interface IFooterService
    {
        Task<ReturnMessage<FooterDTO>> GetFooter();
    }
}
