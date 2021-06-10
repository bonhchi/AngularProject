using AutoMapper;
using Common.Constants;
using Common.Http;
using Domain.DTOs.Contact;
using Domain.DTOs.PageContentContact;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IRepository<Contact> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ReturnMessage<List<ContactDTO>> GetList()
        {
            try
            {
                var entities= _repository.Queryable().OrderByDescending(it => it.CreateByDate).ToList();
                var result = new ReturnMessage<List<ContactDTO>>(false, _mapper.Map<List<Contact>, List<ContactDTO>>(entities), MessageConstants.CreateSuccess);
                return result;
            }
            catch
            {
                return new ReturnMessage<List<ContactDTO>>(true, null, MessageConstants.Error);
            }
        }

        public ReturnMessage<ContactDTO> Update(UpdateContactDTO model)
        {
            try
            {
                var entity = _repository.Find(model.Id); 
                if (entity.IsNotNullOrEmpty() && entity.Status != StatusContact.Done )
                {
                    entity.Update(model);
                    _repository.Update(entity);
                    _unitOfWork.SaveChanges();
                    var result = new ReturnMessage<ContactDTO>(false, _mapper.Map<Contact, ContactDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }   
                return new ReturnMessage<ContactDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<ContactDTO>(true, null, ex.Message);
            }
        }

        public ReturnMessage<ContactDTO> Create(CreateContactDTO model)
        {
            try
            {
                foreach (var item in model.GetType().GetProperties())
                {
                    var key = item.Name;
                    var value = item.GetValue(model, null);
                    if (value.ToString().Trim() == "")
                    {
                        return new ReturnMessage<ContactDTO>(true, null, MessageConstants.Error);
                    }
                }

                var entity = _mapper.Map<CreateContactDTO, Contact>(model);
                entity.Insert();
                _repository.Insert(entity);
                _unitOfWork.SaveChanges();
                var result = new ReturnMessage<ContactDTO>(false, _mapper.Map<Contact, ContactDTO>(entity), MessageConstants.CreateSuccess);
                return result;
            }
            catch
            {
                return new ReturnMessage<ContactDTO>(true, null, MessageConstants.Error);
            }
        }

        public ReturnMessage<ContactDTO> Delete(DeleteContactDTO model)
        {
            try
            {
                var entity = _repository.Find(model.Id);
                if (entity.IsNotNullOrEmpty())
                {
                    entity.Delete();
                    _repository.Delete(entity);
                    _unitOfWork.SaveChanges();
                    var result = new ReturnMessage<ContactDTO>(false, _mapper.Map<Contact, ContactDTO>(entity), MessageConstants.DeleteSuccess);
                    return result;
                }
                return new ReturnMessage<ContactDTO>(true, null, MessageConstants.Error);
            }
            catch (Exception ex)
            {
                return new ReturnMessage<ContactDTO>(true, null, ex.Message);
            }
        }
    }
}
