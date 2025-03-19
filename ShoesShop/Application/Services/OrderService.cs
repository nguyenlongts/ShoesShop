using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Application.Interfaces.Services;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<bool> CreateOrderAsync(Order order)
        {
          var result = await _orderRepository.CreateOrderAsync(order);
          return result != null;
        }

        public Task<bool> DeleteOrderAsync(Guid orderId)
        {
            return _orderRepository.DeleteOrderAsync(orderId);
        }

        public Task<IEnumerable<Order>> GetAllOrdersAsync(int pageNum, int pageSize)
        {
            return _orderRepository.GetAllOrdersAsync(pageNum, pageSize);
        }

        public Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return _orderRepository.GetOrderByIdAsync(orderId);
        }

        public Task<IEnumerable<Order>> GetOrdersByUserAsync(string userId, int pageNum, int pageSize)
        {
            return _orderRepository.GetOrdersByUserIdAsync(userId);
        }

        public Task<bool> UpdateOrderStatusAsync(Guid orderId, Order.OrderStatus newStatus)
        {
            return _orderRepository.UpdateOrderStatusAsync(orderId, newStatus);
        }
    }
}
