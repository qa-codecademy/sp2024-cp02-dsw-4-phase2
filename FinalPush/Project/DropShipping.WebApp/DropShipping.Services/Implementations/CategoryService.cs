using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
using DropShipping.Dto.CategoryDtos;
using DropShipping.Dto.ProductDtos;
using DropShipping.Mappers;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.CustomExceptions;
using Microsoft.EntityFrameworkCore;

namespace DropShipping.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IUserRepository _userRepository;

        public CategoryService(IRepository<Category> categoryRepository, IUserRepository userRepository)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public List<CategoryDto> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll()
                                        .Include(c => c.Products)
                                        .ToList();

            var categoryDtos = categories.Select(c => c.ToCategoryDto()).ToList();

            return categoryDtos;
        }

        public CategoryDto GetCategoryById(int id)
        {
            Category categoryDb = _categoryRepository.GetById(id);
            if (categoryDb == null)
            {
                throw new NotFoundException($"Category with id: {id} was not found!");
            }

            CategoryDto categoryDto = categoryDb.ToCategoryDto();
            return categoryDto;
        }

        public void UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            Category categoryDb = _categoryRepository.GetById(updateCategoryDto.Id);
            if (categoryDb == null)
            {
                throw new NotFoundException($"Category with ID: {updateCategoryDto.Id} was not found!");
            }

            User userDb = _userRepository.GetById(updateCategoryDto.UserId);
            if (userDb == null)
            {
                throw new NotFoundException($"User with ID: {updateCategoryDto.UserId} was not found");
            }

            if (string.IsNullOrEmpty(updateCategoryDto.Name))
            {
                throw new NoDataException("Name: Name is required field");
            }

            categoryDb.Name = updateCategoryDto.Name;

            // Update existing products and add new products
            foreach (var productDto in updateCategoryDto.ProductDtos)
            {
                if (productDto.Id != 0)
                {
                    var existingProduct = categoryDb.Products.FirstOrDefault(p => p.Id == productDto.Id);
                    if (existingProduct != null)
                    {
                        existingProduct.Name = productDto.Name;
                        existingProduct.Description = productDto.Description;
                        existingProduct.Price = productDto.Price;
                        existingProduct.Quantity = productDto.Quantity;
                    }
                }
                else
                {
                    var newProduct = new Product
                    {
                        Name = productDto.Name,
                        Description = productDto.Description,
                        Price = productDto.Price,
                        Quantity = productDto.Quantity,
                        CategoryId = categoryDb.Id
                    };

                    categoryDb.Products.Add(newProduct);
                }
            }

            _categoryRepository.Update(categoryDb);
        }

        public void AddCategory(AddCategoryDto addCategoryDto)
        {
            User userDb = _userRepository.GetById(addCategoryDto.UserId);
            if (userDb == null)
            {
                throw new NoDataException($"User with ID: {addCategoryDto.UserId} does not exist!");
            }

            if (string.IsNullOrEmpty(addCategoryDto.Name))
            {
                throw new NoDataException("Name: Name is required field");
            }

            var category = new Category
            {
                Name = addCategoryDto.Name,
                Products = addCategoryDto.ProductDtos?.Select(p => new Product
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList() ?? new List<Product>()
            };

            _categoryRepository.Add(category);
        }

        public void DeleteCategory(int id)
        {
            Category categoryDb = _categoryRepository.GetById(id);
            if (categoryDb == null)
            {
                throw new NoDataException($"Order with ID: {id} was not found!");
            }

            _categoryRepository.Delete(categoryDb);
        }
    }
}
