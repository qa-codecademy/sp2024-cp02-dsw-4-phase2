using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
using DropShipping.Dto.ProductDtos;
using DropShipping.Mappers;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.CustomExceptions;
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
            var productsDb = _productRepository.GetAll().Where(x => x.UserId == userId);
            if (!productsDb.Any())
            {
                return new List<ProductDto>();
            }

            return productsDb.Select(x => x.ToProductDto()).ToList();
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

            if (string.IsNullOrEmpty(addProductDto.Category.Name))
            {
                throw new NoDataException("Category Name: Category name is required field");
            }
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
