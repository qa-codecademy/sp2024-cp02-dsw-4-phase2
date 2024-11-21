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
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<OrderProduct> _orderProductRepository;
        private readonly IProductRepository _productRepositoryInt;
        private readonly IUserRepository _userRepository;

        public OrderService(IRepository<Order> orderRepository, IUserRepository userRepository, IRepository<OrderProduct> orderProduct, IRepository<Product> productRepository,
            IProductRepository productrepoint)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _orderProductRepository = orderProduct;
            _productRepository = productRepository;
            _productRepositoryInt = productrepoint;
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

        //public void AddOrder(AddOrderDto addOrderDto)
        //{
        //    User userDb = _userRepository.GetById(addOrderDto.UserId);
        //    if (userDb == null)
        //    {
        //        throw new NoDataException($"User with ID: {addOrderDto.UserId} does not exist!");
        //    }

        //    // Fetch products based on the provided product IDs
        //    var products = _productRepositoryInt.GetByIds(addOrderDto.ProductIds);
        //    if (products == null || !products.Any())
        //    {
        //        throw new NoDataException("One or more products do not exist!");
        //    }

        //    // Create a new order object
        //    var order = new Order
        //    {
        //        UserId = addOrderDto.UserId,
        //        OrderProducts = products.Select(product => new OrderProduct
        //        {
        //            ProductsId = product.Id,
        //            Product = product
        //        }).ToList()
        //    };

        //    foreach (var orderProduct in order.OrderProducts)
        //    {
        //        orderProduct.Order = order;  // Link the Order to OrderProduct
        //    }

        //    _orderRepository.Add(order);
        //}
        public void AddOrder(AddOrderDto addOrderDto)
        {
            Console.WriteLine("ProductIds: " + string.Join(",", addOrderDto.ProductIds));

            // Ensure user is fetched correctly
            Console.WriteLine("AddOrderDto.UserId before DB query: " + addOrderDto.UserId);

            User userDb = _userRepository.GetById(addOrderDto.UserId);
            if (userDb == null)
            {
                throw new NoDataException($"User with ID: {addOrderDto.UserId} does not exist!");
            }

            // Ensure products are fetched correctly, and handle null case
            var products = _productRepositoryInt.GetByIds(addOrderDto.ProductIds);
            if (products == null || !products.Any())
            {
                throw new NoDataException("One or more products do not exist!");
            }

            // Create the order with the products
            var order = new Order
            {
                UserId = addOrderDto.UserId,
                OrderProducts = products.Select(product => new OrderProduct
                {
                    ProductsId = product.Id,
                    Product = product
                }).ToList()
            };

            // Link Order to OrderProduct
            foreach (var orderProduct in order.OrderProducts)
            {
                orderProduct.Order = order;
            }

            // Add order to repository
            _orderRepository.Add(order);
        }


        //public void DeleteOrder(int orderId, int userId)
        //{
        //    var order = _orderRepository.GetAll()
        //                                .Include(o => o.OrderProducts)
        //                                .ThenInclude(op => op.Product)
        //                                .FirstOrDefault(o => o.Id == orderId);

        //    if (order == null)
        //    {
        //        throw new NotFoundException($"Order with ID: {orderId} was not found!");
        //    }

        //    if (order.UserId != userId)
        //    {
        //        throw new UnauthorizedAccessException("You do not have access to this order.");
        //    }

        //    _orderRepository.Delete(order);
        //}
        public void DeleteOrder(int orderId, int userId)
        {
            // Retrieve the order with related data
            var order = _orderRepository.GetAll()
                                        .Include(o => o.OrderProducts)
                                        .ThenInclude(op => op.Product)
                                        .FirstOrDefault(o => o.Id == orderId);

            // If the order does not exist
            if (order == null)
            {
                throw new NotFoundException($"Order with ID: {orderId} was not found!");
            }

            // Check if the order belongs to the authenticated user
            if (order.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have access to this order.");
            }

            // Delete the order
            _orderRepository.Delete(order);
        }


        public void RemoveProductFromOrder(int orderId, int productId, int userId)
        {
            var order = _orderRepository.GetAll()
                                        .FirstOrDefault(o => o.Id == orderId && o.UserId == userId);

            if (order == null)
            {
                throw new NotFoundException($"Order with ID {orderId} not found.");
            }

            var orderProduct = _orderProductRepository.GetAll()
                                                      .FirstOrDefault(op => op.OrdersId == orderId && op.ProductsId == productId);

            if (orderProduct == null)
            {
                throw new NotFoundException($"Product with ID {productId} not found in the order.");
            }

            _orderProductRepository.Delete(orderProduct);
        }

        public async Task<Order> CreateOrderAsync(int userId, List<int> productIds)
        {
            var order = new Order
            {
                UserId = userId
            };

            foreach (var productId in productIds)
            {
                var product = _productRepository.GetById(productId);
                if (product != null)
                {
                    order.OrderProducts.Add(new OrderProduct
                    {
                        ProductsId = productId,
                        Order = order
                    });
                }
            }

            _orderRepository.Add(order);
            return order;
        }
    }
}
