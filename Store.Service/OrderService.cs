using Store.Data.Infrastructure;
using Store.Data.Repositories;
using Store.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Service
{
    public interface IOrderService
    {
        void Update(Order order);
        bool Create(Order order, List<OrderDetail> orderDetails);
        IQueryable<Order> GetAllOrder();
        IQueryable<Order> GetOrderByUserId(string userId, int page, int pageSize, out int totalRow);
        Order GetById(int id);
        void Save();
        Order Delete(int id);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public bool Create(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();

                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderId = order.Id;
                    _orderDetailRepository.Add(orderDetail);
                }



                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<Order> GetAllOrder()
        {
            return _orderRepository.GetAllOrder();
        }



        public IQueryable<Order> GetOrderByUserId(string userId, int page, int pageSize, out int totalRow)
        {
            var query = _orderRepository.GetOrderByUserId(userId);
            totalRow = query.Count();
            return query.OrderByDescending(x=>x.Id).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public Order GetById(int id)
        {
            return _orderRepository.GetSingleById(id);
        }

        public void Update(Order order)
        {
            _orderRepository.Update(order);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public Order Delete(int id)
        {
            return _orderRepository.Delete(id);
        }


    }
}
