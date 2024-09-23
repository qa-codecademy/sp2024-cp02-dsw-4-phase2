using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
using DropShipping.Dto.OrderDtos;
using DropShipping.Mappers;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.CustomExceptions.OrderCustomException;
using DropShipping.Shared.CustomExceptions.ProductCutsomException;
using DropShipping.Shared.CustomExceptions.UserException;

namespace DropShipping.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IRepository<Order> orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public List<OrderDto> GetAllOrders(int userId)
        {
            var orderDb = _orderRepository.GetAll().Where(x => x.UserId == userId);
            if (orderDb.Any())
            {
                return new List<OrderDto>();
            }

            return orderDb.Select(x => x.ToOrderDto()).ToList();
        }

        public OrderDto GetOrderById(int id)
        {
            Order orderDb = _orderRepository.GetById(id);
            if (orderDb == null)
            {
                throw new OrderNotFoundException($"Order with id: {id} was not found!");
            }

            OrderDto orderDto = orderDb.ToOrderDto();
            return orderDto;
        }

        public void UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            Order orderDb = _orderRepository.GetById(updateOrderDto.Id);
            if (orderDb == null)
            {
                throw new OrderNotFoundException($"Order with ID: {updateOrderDto.Id} was not found!");
            }

            User userDb = _userRepository.GetById(updateOrderDto.UserId);
            if (userDb == null)
            {
                throw new OrderNotFoundException($"User with ID: {updateOrderDto.UserId} was not found");
            }

            if ((string.IsNullOrEmpty(updateOrderDto.FirstName)) || (string.IsNullOrEmpty(updateOrderDto.LastName)))
            {
                throw new UserDataException("Firstname & Lastname are required fields");
            }

            if (string.IsNullOrEmpty(updateOrderDto.Address))
            {
                throw new UserDataException("Address: Address is required field");
            }

            if (string.IsNullOrEmpty(updateOrderDto.CardNumber))
            {
                throw new UserDataException("CardNumber: CardNumber is required field");
            }

            if (string.IsNullOrEmpty(updateOrderDto.City))
            {
                throw new UserDataException("City: City is required field");
            }

            orderDb.FirstName = updateOrderDto.FirstName;
            orderDb.LastName = updateOrderDto.LastName;
            orderDb.Address = updateOrderDto.Address;
            orderDb.CardNumber = updateOrderDto.CardNumber;
            orderDb.City = updateOrderDto.City;
        }

        public void AddOrder(AddOrderDto addOrderDto)
        {
            User userDb = _userRepository.GetById(addOrderDto.UserId);
            if (userDb == null)
            {
                throw new OrderDataException($"User with ID: {addOrderDto.UserId} does not exist!");
            }

            if (string.IsNullOrEmpty(addOrderDto.FirstName) || string.IsNullOrEmpty(addOrderDto.LastName))
            {
                throw new OrderDataException("Name: Firstname & Lastname are required field!");
            }

            if (string.IsNullOrEmpty(addOrderDto.CardNumber))
            {
                throw new OrderDataException("CardNumber: CardNumber is required field!");
            }

            if (string.IsNullOrEmpty(addOrderDto.Address))
            {
                throw new OrderDataException("Address: Address is required field!");
            }

            if (string.IsNullOrEmpty(addOrderDto.City))
            {
                throw new OrderDataException("City: City is required field!");
            }
        }

        public void DeleteOrder(int id)
        {
            Order orderDb = _orderRepository.GetById(id);
            if (orderDb == null)
            {
                throw new OrderNotFoundException($"Order with ID: {id} was not found!");
            }

            _orderRepository.Delete(orderDb);
        }
    }
}
