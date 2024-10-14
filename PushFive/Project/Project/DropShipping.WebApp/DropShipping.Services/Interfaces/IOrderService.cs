using DropShipping.Dto.OrderDtos;

namespace DropShipping.Services.Interfaces
{
    public interface IOrderService
    {
        List<OrderDto> GetAllOrders(int userId);

        OrderDto GetOrderById(int id);

        void AddOrder(AddOrderDto order);

        void UpdateOrder(UpdateOrderDto order);

        void DeleteOrder(int id);
    }
}
