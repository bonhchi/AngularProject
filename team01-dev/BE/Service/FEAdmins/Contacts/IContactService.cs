using Common.Http;
using Domain.DTOs.Contact;
using Domain.DTOs.PageContentContact;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Contacts
{
    public interface IContactService: ICommonCRUDService<ContactDTO,CreateContactDTO,UpdateContactDTO,DeleteContactDTO>
    {
        public ReturnMessage<List<ContactDTO>> GetList();
    }
}
