using Common.Http;
using Domain.DTOs.InfomationWeb;
using System.Threading.Tasks;

namespace Service.InformationWebsiteServices
{
    public interface IInfomationWebService
    {
        Task<ReturnMessage<InformationWebDTO>> GetInfo();
        Task<ReturnMessage<InformationWebDTO>> UpdateAsync(UpdateInformationWebDTO model);
    }
}
