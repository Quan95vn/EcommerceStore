using Store.Data.Infrastructure;
using Store.Data.Repositories;
using Store.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Store.Service
{
    public interface IContactDetailService
    {
        ContactDetail Add(ContactDetail contactDetail);

        ContactDetail Delete(int id);

        void Update(ContactDetail contactDetail);

        IQueryable<ContactDetail> GetAll();

        IQueryable<ContactDetail> GetAll(string keyword);

        ContactDetail GetById(int id);

        ContactDetail GetDefaultContact();

        void Save();
    }

    public class ContactDetailService : IContactDetailService
    {
        private readonly IContactDetailRepository _contactDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ContactDetailService(IContactDetailRepository contactDetailRepository, IUnitOfWork unitOfWork)
        {
            _contactDetailRepository = contactDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public ContactDetail Add(ContactDetail contactDetail)
        {
            return _contactDetailRepository.Add(contactDetail);
        }

        public ContactDetail Delete(int id)
        {
            return _contactDetailRepository.Delete(id);
        }

        public IQueryable<ContactDetail> GetAll()
        {
            return _contactDetailRepository.GetAll();
        }

        public IQueryable<ContactDetail> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _contactDetailRepository.GetMulti(x => x.Name.Contains(keyword));
            }
            else
            {
                return _contactDetailRepository.GetAll();
            }
        }

        public ContactDetail GetById(int id)
        {
            return _contactDetailRepository.GetSingleById(id);
        }

        public ContactDetail GetDefaultContact()
        {
            return _contactDetailRepository.GetSingleByCondition(x => x.Status == true);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ContactDetail contactDetail)
        {
            _contactDetailRepository.Update(contactDetail);
        }
    }
}
