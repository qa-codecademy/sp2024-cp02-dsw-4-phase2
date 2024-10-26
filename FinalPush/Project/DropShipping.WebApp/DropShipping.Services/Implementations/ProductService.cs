using DropShipping.DataBase.Implementations.EFImpementations;
using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
using DropShipping.Dto.OrderDtos;
using DropShipping.Dto.ProductDtos;
using DropShipping.Mappers;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.CustomExceptions;
using MailKit.Search;
using System.Data;

namespace DropShipping.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IUserRepository _userRepository;

        public ProductService(IRepository<Product> productRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }


        public List<ProductDto> GetAllProducts(int userId)
        {
            var productsDb = _productRepository.GetAll()
                                               .Select(x => x.ToProductDto())
                                               .ToList();

            if (!productsDb.Any())
            {
                throw new NoDataException("No products found for the specified user.");
            }

            return productsDb;
        }
    
        public ProductDto GetProductById(int id)
        {
            Product productDb = _productRepository.GetById(id);
            if (productDb == null)
            {
                throw new NotFoundException($"Product with id: {id} was not found!");
            }

            ProductDto productDto = productDb.ToProductDto();
            return productDto;
        }

        public void UpdateProduct(UpdateProductDto updateProductDto)
        {
            Product productDb = _productRepository.GetById(updateProductDto.Id);
            if (productDb == null)
            {
                throw new NotFoundException($"Product with ID: {updateProductDto.Id} was not found!");
            }

            User userDb = _userRepository.GetById(updateProductDto.UserId);
            if (userDb == null)
            {
                throw new NotFoundException($"User with ID: {updateProductDto.UserId} was not found");
            }

            if (string.IsNullOrEmpty(updateProductDto.Name))
            {
                throw new NoDataException("Name: Name is required field!");
            }

            if (string.IsNullOrEmpty(updateProductDto.Description))
            {
                throw new NoDataException("Description: Description is required field!");
            }

            if (updateProductDto.Quantity == null)
            {
                throw new NoDataException("Quantity: Quantity is required field!");
            }

            if (updateProductDto.Price == null)
            {
                throw new NoDataException("Price: Price is required field!");
            }

            productDb.Name = updateProductDto.Name;
            productDb.Description = updateProductDto.Description;
            productDb.Quantity = updateProductDto.Quantity;
            productDb.Price = updateProductDto.Price;
            productDb.CategoryId = updateProductDto.CategoryId;

            _productRepository.Update(productDb);
        }

        public void AddProduct(AddProductDto addProductDto)
        {
            User userDb = _userRepository.GetById(addProductDto.UserId);
            if (userDb == null)
            {
                throw new NoDataException($"User with ID: {addProductDto.UserId} does not exist!");
            }

            if (string.IsNullOrEmpty(addProductDto.Name))
            {
                throw new NoDataException("Name: Name is required field!");
            }

            if (string.IsNullOrEmpty(addProductDto.Description))
            {
                throw new NoDataException("Description: Description is required field!");
            }

            if (addProductDto.Quantity == null)
            {
                throw new NoDataException("Quantity: Quantity is required field!");
            }

            if (addProductDto.Price == null)
            {
                throw new NoDataException("Price: Price is required field!");
            }

            var product = new Product
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                Price = addProductDto.Price,
                Quantity = addProductDto.Quantity,
                CategoryId = addProductDto.CategoryId
            };

            _productRepository.Add(product);
        }

        public void DeleteProduct(int id)
        {
            Product productDb = _productRepository.GetById(id);
            if (productDb == null)
            {
                throw new NotFoundException($"Product with ID: {id} was not found!");
            }

            _productRepository.Delete(productDb);
        }
    }
}
