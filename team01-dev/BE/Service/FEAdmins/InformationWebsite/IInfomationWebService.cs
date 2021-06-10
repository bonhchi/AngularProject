using Common.Http;
using Domain.DTOs.InfomationWeb;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.InformationWebsiteServices
{
    public interface IInfomationWebService
    {
        ReturnMessage<InformationWebDTO> GetInfo();

        ReturnMessage<InformationWebDTO> Update(UpdateInformationWebDTO model);
    }
}
