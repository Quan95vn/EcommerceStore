using Store.Data.Infrastructure;
using Store.Data.Repositories;
using Store.Model.Models;

namespace Store.Service
{
    public interface IAboutService
    {
        About Add(About about);

        void Save();
    }

    public class AboutService : IAboutService
    {
        private readonly IAboutRepository _aboutRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AboutService(IAboutRepository aboutRepository, IUnitOfWork unitOfWork)
        {
            _aboutRepository = aboutRepository;
            _unitOfWork = unitOfWork;
        }

        public About Add(About about)
        {
            return _aboutRepository.Add(about);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
