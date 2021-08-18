using Common.Http;
using Domain.DTOs.FEUsers.Headers;
using System.Threading.Tasks;

namespace Service.Header
{
    public interface IHeaderService 
    {
        Task<ReturnMessage<HeaderDTO>> GetHeader();
    }
}
