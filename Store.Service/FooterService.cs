using Store.Data.Infrastructure;
using Store.Data.Repositories;
using Store.Model.Models;
using System.Collections.Generic;

namespace Store.Service
{
    public interface IFooterService
    {
        Footer Add(Footer Footer);

        void Update(Footer Footer);

        Footer Delete(int id);

        IEnumerable<Footer> GetAll();

        IEnumerable<Footer> GetAll(string keyword);

        Footer GetById(int id);

        Footer GetLatestFooter();

        void Save();
    }

    public class FooterService : IFooterService
    {
        private readonly IFooterRepository _footerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FooterService(IFooterRepository footerRepository, IUnitOfWork unitOfWork)
        {
            _footerRepository = footerRepository;
            _unitOfWork = unitOfWork;
        }

        public Footer Add(Footer Footer)
        {
            return _footerRepository.Add(Footer);
        }

        public Footer Delete(int id)
        {
            return _footerRepository.Delete(id);
        }

        public IEnumerable<Footer> GetAll()
        {
            return _footerRepository.GetAll();
        }

        public IEnumerable<Footer> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _footerRepository.GetMulti(x => x.Content.Contains(keyword));
            }
            else
            {
                return _footerRepository.GetAll();
            }
        }

        public Footer GetById(int id)
        {
            return _footerRepository.GetSingleById(id);
        }

        public Footer GetLatestFooter()
        {
            return _footerRepository.GetSingleByCondition(x => x.Status == true);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Footer Footer)
        {
             _footerRepository.Update(Footer);
        }
    }
}
