using DropShipping.Dto.OrderDtos;

namespace DropShipping.Services.Interfaces
{
    public interface IOrderService
    {
        List<OrderDto> GetAllOrders(int userId);

        OrderDto GetOrderById(int id, int userId);

        void AddOrder(AddOrderDto order);

        void UpdateOrder(UpdateOrderDto order, int userId);

        void DeleteOrder(int id);
    }
}
