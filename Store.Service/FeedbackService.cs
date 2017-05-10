using Store.Data.Infrastructure;
using Store.Data.Repositories;
using Store.Model.Models;
using System.Collections.Generic;

namespace Store.Service
{
    public interface IFeedbackService
    {
        Feedback Add(Feedback Feedback);

        Feedback Delete(int id);

        IEnumerable<Feedback> GetAll();

        Feedback GetById(int id);

        void Save();
    }

    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
        {
            _feedbackRepository = feedbackRepository;
            _unitOfWork = unitOfWork;
        }

        public Feedback Add(Feedback Feedback)
        {
            return _feedbackRepository.Add(Feedback);
        }

        public Feedback Delete(int id)
        {
            return _feedbackRepository.Delete(id);
        }

        public IEnumerable<Feedback> GetAll()
        {
            return _feedbackRepository.GetAll();
        }

        public Feedback GetById(int id)
        {
            return _feedbackRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

    }
}
