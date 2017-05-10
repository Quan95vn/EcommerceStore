using Store.Data.Infrastructure;
using Store.Data.Repositories;
using Store.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Store.Service
{
    public interface ISlideService
    {
        Slide Add(Slide slide);

        void Update(Slide slide);

        Slide Delete(int id);

        IQueryable<Slide> GetAll();

        IQueryable<Slide> GetAll(string keyword);

        Slide GetById(int id);

        IEnumerable<Slide> GetLatestSlide(int top);

        void Save();
    }

    public class SlideService : ISlideService
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SlideService(ISlideRepository slideRepository, IUnitOfWork unitOfWork)
        {
            _slideRepository = slideRepository;
            _unitOfWork = unitOfWork;
        }

        public Slide Add(Slide slide)
        {
            return _slideRepository.Add(slide);
        }

        public Slide Delete(int id)
        {
            return _slideRepository.Delete(id);
        }

        public IQueryable<Slide> GetAll()
        {
            return _slideRepository.GetAll();
        }

        public IQueryable<Slide> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _slideRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _slideRepository.GetAll();
        }

        public Slide GetById(int id)
        {
            return _slideRepository.GetSingleById(id);
        }

        public IEnumerable<Slide> GetLatestSlide(int top)
        {
            return _slideRepository.GetMulti(x => x. Status == true).OrderByDescending(x => x.DisplayOrder).Take(top);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide slide)
        {
            _slideRepository.Update(slide);
        }
    }
}
