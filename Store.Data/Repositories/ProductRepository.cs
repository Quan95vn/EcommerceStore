using Store.Data.Infrastructure;
using Store.Model.Models;

namespace Store.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
