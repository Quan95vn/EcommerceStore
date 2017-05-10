using Store.Data.Repositories;
using Store.Model.Models;

namespace Store.Service
{
    public interface IOrderDetailService
    {
        OrderDetail GetById(int id);
    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public OrderDetail GetById(int id)
        {
            return _orderDetailRepository.GetSingleById(id);
        }
    }
}
