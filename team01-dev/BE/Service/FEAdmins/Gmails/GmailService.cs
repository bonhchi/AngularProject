using AutoMapper;
using Common.Constants;
using Common.Http;
using Domain.DTOs.Mails;
using Infrastructure.Mails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Gmails
{
    public class GmailService : IGmailService
    {
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public GmailService(IEmailService emailService, IMapper mapper)
        {
            _emailService = emailService;
            _mapper = mapper;
        }

        public ReturnMessage<bool> ReplyMail(ReplyEmailDTO dto)
        {
            try
            {
                var emailMessage = _mapper.Map<ReplyEmailDTO, EmailMessage>(dto);
                _emailService.Send(emailMessage);
                return new ReturnMessage<bool>(false, true, MessageConstants.GetSuccess);
            }catch(Exception ex)
            {
                return new ReturnMessage<bool>(true, false, ex.Message);
            }
        }

        public ReturnMessage<bool> SendMail(SendMailDTO dto)
        {
            try
            {
                var emailMessage = _mapper.Map<SendMailDTO, EmailMessage>(dto);
                _emailService.Send(emailMessage);
                return new ReturnMessage<bool>(false, true, MessageConstants.GetSuccess);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<bool>(true, false, ex.Message);
            }
        }
    }
}
