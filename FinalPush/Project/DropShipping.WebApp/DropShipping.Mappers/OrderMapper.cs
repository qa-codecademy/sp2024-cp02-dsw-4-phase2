using DropShipping.Domain.Models;
using DropShipping.Dto.OrderDtos;
using DropShipping.Dto.ProductDtos;

namespace DropShipping.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                UserId = order.UserId,
                UserFullName = order.User != null
            ? $"{order.User.FirstName} {order.User.LastName}"
            : "Unknown User",
                ProductDtos = order.OrderProducts != null
            ? order.OrderProducts.Select(op => new ProductDto
            {
                Id = op.Product.Id,
                Name = op.Product.Name,
                Description = op.Product.Description,
                Price = op.Product.Price,
                Quantity = op.Product.Quantity,
                CategoryId = op.Product.CategoryId
            }).ToList()
            : new List<ProductDto>()
            };
        }

        public static Order ToOrder(this AddOrderDto addedOrder)
        {
            var order = new Order()
            {
                UserId = addedOrder.UserId,
                OrderProducts = addedOrder.ProductIds.Select(productId => new OrderProduct
                {
                    ProductsId = productId
                }).ToList()
            };

            return order;
        }
    }
}
