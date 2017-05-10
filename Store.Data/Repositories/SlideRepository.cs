using Store.Data.Infrastructure;
using Store.Model.Models;

namespace Store.Data.Repositories
{
    public interface ISlideRepository : IRepository<Slide>
    {

    }

    public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
    {
        public SlideRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
