using API_ShoesShop.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Interfaces.Repositories;
using ShoesShop.Domain.Entities;

namespace ShoesShop.Infrastructure.Repositories.Implement
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDBContext _context;
        public OrderRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<bool> AddOrderItemAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            var result = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var existOrder = await _context.Orders.FindAsync(orderId);
            if (existOrder == null) return false;
            _context.Orders.Remove(existOrder);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(int pageNum=1,int pageSize=5)
        {
            var result = await _context.Orders.OrderByDescending(o=>o.CreateAt).Include(o=>o.OrderItems).Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();
            return result;
        }

        public Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            var existOrder = _context.Orders.Include(o => o.OrderItems).ThenInclude(oi=>oi.ProductDetail).Include(o=>o.User).FirstOrDefaultAsync(o => o.OrderId == orderId);
            return existOrder;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(Guid orderId)
        {
            var result = await _context.OrderItems.Where(oi => oi.OrderId == orderId).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            var result = await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
            return result;
        }

        public Task<bool> OrderExistsAsync(Guid orderId)
        {
            var existOrder = _context.Orders.AnyAsync(o => o.OrderId == orderId);
            return existOrder;
        }

        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, Order.OrderStatus status)
        {
            var existOrder = await _context.Orders.FindAsync(orderId);
            if (existOrder == null) return false;
            existOrder.Status = status;
            _context.Orders.Update(existOrder);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
