using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
using DropShipping.Dto.OrderDtos;
using DropShipping.Mappers;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.CustomExceptions;
using System.Data;

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
            var orderDb = _orderRepository.GetAll()
                                          .Where(x => x.UserId == userId)
                                          .Select(x => x.ToOrderDto())
                                          .ToList();
            if (!orderDb.Any())
            {
                return new List<OrderDto>();
            }

            return orderDb;
        }

        public OrderDto GetOrderById(int id)
        {
            Order orderDb = _orderRepository.GetById(id);
            if (orderDb == null)
            {
                throw new NotFoundException($"Order with id: {id} was not found!");
            }

            OrderDto orderDto = orderDb.ToOrderDto();
            return orderDto;
        }

        public void UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            Order orderDb = _orderRepository.GetById(updateOrderDto.Id);
            if (orderDb == null)
            {
                throw new NotFoundException($"Order with ID: {updateOrderDto.Id} was not found!");
            }

            User userDb = _userRepository.GetById(updateOrderDto.UserId);
            if (userDb == null)
            {
                throw new NotFoundException($"User with ID: {updateOrderDto.UserId} was not found");
            }

            if ((string.IsNullOrEmpty(updateOrderDto.FirstName)) || (string.IsNullOrEmpty(updateOrderDto.LastName)))
            {
                throw new NoDataException("Firstname & Lastname are required fields");
            }

            if (string.IsNullOrEmpty(updateOrderDto.Address))
            {
                throw new NoDataException("Address: Address is required field");
            }

            if (string.IsNullOrEmpty(updateOrderDto.CardNumber))
            {
                throw new NoDataException("CardNumber: CardNumber is required field");
            }

            if (string.IsNullOrEmpty(updateOrderDto.City))
            {
                throw new NoDataException("City: City is required field");
            }

            orderDb.FirstName = updateOrderDto.FirstName;
            orderDb.LastName = updateOrderDto.LastName;
            orderDb.Address = updateOrderDto.Address;
            orderDb.CardNumber = updateOrderDto.CardNumber;
            orderDb.City = updateOrderDto.City;

            _orderRepository.Update(orderDb);
        }

        public void AddOrder(AddOrderDto addOrderDto)
        {
            User userDb = _userRepository.GetById(addOrderDto.UserId);
            if (userDb == null)
            {
                throw new NoDataException($"User with ID: {addOrderDto.UserId} does not exist!");
            }

            if (string.IsNullOrEmpty(addOrderDto.FirstName) || string.IsNullOrEmpty(addOrderDto.LastName))
            {
                throw new NoDataException("Name: Firstname & Lastname are required field!");
            }

            if (string.IsNullOrEmpty(addOrderDto.CardNumber))
            {
                throw new NoDataException("CardNumber: CardNumber is required field!");
            }

            if (string.IsNullOrEmpty(addOrderDto.Address))
            {
                throw new NoDataException("Address: Address is required field!");
            }

            if (string.IsNullOrEmpty(addOrderDto.City))
            {
                throw new NoDataException("City: City is required field!");
            }

            var order = new Order
            {
                UserId = addOrderDto.UserId,
                FirstName = addOrderDto.FirstName,
                LastName = addOrderDto.LastName,
                Address = addOrderDto.Address,
                CardNumber = addOrderDto.CardNumber,
                City = addOrderDto.City,
                PostalCode = addOrderDto.PostalCode
            };

            _orderRepository.Add(order);
        }

        public void DeleteOrder(int id)
        {
            Order orderDb = _orderRepository.GetById(id);
            if (orderDb == null)
            {
                throw new NotFoundException($"Order with ID: {id} was not found!");
            }

            _orderRepository.Delete(orderDb);
        }
    }
}
