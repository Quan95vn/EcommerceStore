using Store.Data.Infrastructure;
using Store.Model.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Store.Common.ViewModel;
using System.Data.SqlClient;

namespace Store.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<Order> GetAllOrder();
        IQueryable<Order> GetOrderByUserId(string userId);
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IQueryable<Order> GetAllOrder()
        {
            return DbContext.Orders
               .Include(o => o.OrderDetails.Select(od => od.Product));
        }

        public IQueryable<Order> GetOrderByUserId(string userId )
        {
            return DbContext.Orders
                .Include(o => o.OrderDetails.Select(od => od.Product))
                .Where(o => o.CustomerId == userId);
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate)
            };
            return DbContext.Database.SqlQuery<RevenueStatisticViewModel>("GetRevenueStatistic @fromDate,@toDate", parameters);
        }
    }
}
