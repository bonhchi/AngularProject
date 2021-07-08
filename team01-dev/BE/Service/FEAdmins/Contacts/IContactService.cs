using Common.Http;
using Domain.DTOs.Contact;
using Domain.DTOs.PageContentContact;
using Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contacts
{
    public interface IContactService: ICommonCRUDService<ContactDTO,CreateContactDTO,UpdateContactDTO,DeleteContactDTO>
    {
        Task<ReturnMessage<List<ContactDTO>>> GetList();
    }
}
