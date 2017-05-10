using Store.Data.Infrastructure;
using Store.Model.Models;

namespace Store.Data.Repositories
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {

    }

    public class FeedbackRepository : RepositoryBase<Feedback>,IFeedbackRepository
    {
        public FeedbackRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
