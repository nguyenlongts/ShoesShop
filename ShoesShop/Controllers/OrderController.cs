using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.DTOs;
using ShoesShop.Application.Interfaces.Services;
using ShoesShop.Domain.Entities;
using static ShoesShop.Domain.Entities.Order;

namespace ShoesShop.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            if (createOrderDto == null || createOrderDto.OrderItems.Count == 0)
            {
                return BadRequest("Dữ liệu đơn hàng không hợp lệ.");
            }

            var order = new Order
            {
                UserId = createOrderDto.UserId,
                ShippingAddress = createOrderDto.ShippingAddress,
                OrderItems = createOrderDto.OrderItems.Select(item => new OrderItem
                {
                    ProductDetailId = item.ProductDetailId,
                    Quantity = item.Quantity,
                    UnitPrice = item.PriceAtOrder, 
                }).ToList(),
                CreateAt = DateTime.UtcNow
            };

            var result = await _orderService.CreateOrderAsync(order);
            if (!result) return BadRequest("Không thể tạo đơn hàng.");
            return Ok("Đơn hàng đã được tạo thành công.");
        }



        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] int pageNum = 1, [FromQuery] int pageSize = 10)
        {
            var orders = await _orderService.GetAllOrdersAsync(pageNum, pageSize);
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound("Đơn hàng không tồn tại.");
            var detail = new OrderDetailResponse
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                Fullname = order.User.FirstName+" "+order.User.LastName,
                ShippingAddress = order.ShippingAddress,
                CreateAt = order.CreateAt,
                Status = order.Status,
                OrderItems = order.OrderItems,
                TotalPrice=order.TotalPrice,
                PhoneNumber = order.User.PhoneNumber
            };
            return Ok(detail);
        }


        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusRequest request)
        {
            var result = await _orderService.UpdateOrderStatusAsync(request.OrderId, ((Order.OrderStatus)request.Status));
            if (!result) return BadRequest("Cập nhật trạng thái thất bại.");
            return Ok("Cập nhật trạng thái thành công.");
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            var result = await _orderService.DeleteOrderAsync(orderId);
            if (!result) return BadRequest("Xóa đơn hàng thất bại.");
            return Ok("Đơn hàng đã được xóa.");
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(string userId, [FromQuery] int pageNum = 1, [FromQuery] int pageSize = 10)
        {
            var orders = await _orderService.GetOrdersByUserAsync(userId, pageNum, pageSize);
            return Ok(orders);
        }


        //[HttpGet("status/{status}")]
        //public async Task<IActionResult> GetOrdersByStatus(OrderStatus status, [FromQuery] int pageNum = 1, [FromQuery] int pageSize = 10)
        //{
        //    var orders = await _orderService.GetOrdersByStatusAsync(status, pageNum, pageSize);
        //    return Ok(orders);
        //}
    }

}
