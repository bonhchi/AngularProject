using Common.Http;
using System.Threading.Tasks;

namespace Service.Common
{
    public interface ICommonCRUDService<TResponse, CreateT, UpdateT, DeleteT> where TResponse : class
    {
        Task<ReturnMessage<TResponse>> CreateAsync(CreateT model);
        Task<ReturnMessage<TResponse>> UpdateAsync(UpdateT model);
        Task<ReturnMessage<TResponse>> DeleteAsync(DeleteT model);
    }

    public interface ICommonCRUDService<TResponse> where TResponse : class
    {

    }

    public interface ICommonCRUDService<TResponse, UpdateT> where TResponse : class
    {
        Task<ReturnMessage<TResponse>> UpdateAsync(UpdateT model);
    }

}
