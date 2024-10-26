using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
using DropShipping.Dto.OrderDtos;
using DropShipping.Mappers;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.CustomExceptions;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
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
                                          .Include(o => o.User)
                                          .Where(x => x.UserId == userId)
                                          .Select(x => x.ToOrderDto())
                                          .ToList();
            if (!orderDb.Any())
            {
                throw new NoDataException("You don't have any orders yet.");
            }

            return orderDb;
        }

        public OrderDto GetOrderById(int orderId, int userId)
        {
            var order = _orderRepository.GetAll()
                                .Include(o => o.User)
                                .Include(o => o.OrderProducts)
                                .ThenInclude(op => op.Product)
                                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                throw new NotFoundException($"Order with ID: {orderId} was not found!");
            }

            if (order.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have access to this order.");
            }

            return order.ToOrderDto();
        }

        //public void UpdateOrder(UpdateOrderDto updateOrderDto)
        //{
        //    Order orderDb = _orderRepository.GetById(updateOrderDto.Id);
        //    if (orderDb == null)
        //    {
        //        throw new NotFoundException($"Order with ID: {updateOrderDto.Id} was not found!");
        //    }

        //    User userDb = _userRepository.GetById(updateOrderDto.UserId);
        //    if (userDb == null)
        //    {
        //        throw new NotFoundException($"User with ID: {updateOrderDto.UserId} was not found");
        //    }

        //    orderDb.UserId = updateOrderDto.UserId;

        //    if (updateOrderDto.ProductIds != null)
        //    {
        //        var productsToRemove = orderDb.OrderProducts
        //            .Where(op => !updateOrderDto.ProductIds.Contains(op.ProductsId))
        //            .ToList();

        //        foreach (var product in productsToRemove)
        //        {
        //            orderDb.OrderProducts.Remove(product);
        //        }

        //        foreach (var productId in updateOrderDto.ProductIds)
        //        {
        //            if (!orderDb.OrderProducts.Any(op => op.ProductsId == productId))
        //            {
        //                orderDb.OrderProducts.Add(new OrderProduct
        //                {
        //                    ProductsId = productId,
        //                    OrdersId = orderDb.Id
        //                });
        //            }
        //        }
        //    }
        //    _orderRepository.Update(orderDb);
        //}

        public void UpdateOrder(UpdateOrderDto updateOrderDto, int userId)
        {
            Order orderDb = _orderRepository.GetById(updateOrderDto.Id);

            if (orderDb == null)
            {
                throw new NotFoundException($"Order with ID: {updateOrderDto.Id} was not found!");
            }

            if (orderDb.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to update this order.");
            }

            User userDb = _userRepository.GetById(updateOrderDto.UserId);
            if (userDb == null)
            {
                throw new NotFoundException($"User with ID: {updateOrderDto.UserId} was not found");
            }

            orderDb.UserId = updateOrderDto.UserId;

            if (updateOrderDto.ProductIds != null)
            {
                var productsToRemove = orderDb.OrderProducts
                    .Where(op => !updateOrderDto.ProductIds.Contains(op.ProductsId))
                    .ToList();

                foreach (var product in productsToRemove)
                {
                    orderDb.OrderProducts.Remove(product);
                }

                foreach (var productId in updateOrderDto.ProductIds)
                {
                    if (!orderDb.OrderProducts.Any(op => op.ProductsId == productId))
                    {
                        orderDb.OrderProducts.Add(new OrderProduct
                        {
                            ProductsId = productId,
                            OrdersId = orderDb.Id
                        });
                    }
                }
            }

            _orderRepository.Update(orderDb);
        }

        public void AddOrder(AddOrderDto addOrderDto)
        {
            User userDb = _userRepository.GetById(addOrderDto.UserId);
            if (userDb == null)
            {
                throw new NoDataException($"User with ID: {addOrderDto.UserId} does not exist!");
            }

            var order = new Order
            {
                UserId = addOrderDto.UserId,
                OrderProducts = addOrderDto.ProductIds.Select(productId => new OrderProduct
                {
                    ProductsId = productId
                }).ToList()
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
