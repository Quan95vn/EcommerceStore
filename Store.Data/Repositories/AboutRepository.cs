using Store.Data.Infrastructure;
using Store.Model.Models;

namespace Store.Data.Repositories
{
    public interface IAboutRepository : IRepository<About>
    {

    }

    public class AboutRepository : RepositoryBase<About>, IAboutRepository
    {
        public AboutRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
