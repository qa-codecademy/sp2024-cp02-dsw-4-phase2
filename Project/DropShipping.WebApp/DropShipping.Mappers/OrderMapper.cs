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
                Address = order.Address,
                City = order.City,
                FirstName = order.FirstName,
                LastName = order.LastName,
                ProductDtos = order.Products.Select(product => new ProductDto
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity
                }).ToList()
            };
        }

        public static Order ToOrder(this AddOrderDto addedOrder)
        {
            return new Order()
            {
                FirstName = addedOrder.FirstName,
                LastName = addedOrder.LastName,
                Address = addedOrder.Address,
                City = addedOrder.City,
                CardNumber = addedOrder.CardNumber,
                PostalCode = addedOrder.PostalCode,
                UserId = addedOrder
            };
        }
    }
}
