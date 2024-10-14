using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
using DropShipping.Dto.CategoryDtos;
using DropShipping.Dto.ProductDtos;
using DropShipping.Mappers;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.CustomExceptions;

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

        public List<CategoryDto> GetAllCategories(int userId)
        {
            var categoryDb = _categoryRepository.GetAll()
                                                .Where(x => x.UserId == userId)
                                                .Select(x => x.ToCategoryDto())
                                                .ToList();

            if (!categoryDb.Any())
            {
                return new List<CategoryDto>();
            }

            return categoryDb;
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

            categoryDb.Id = updateCategoryDto.Id;
            categoryDb.Name = updateCategoryDto.Name;
            categoryDb.UserId = updateCategoryDto.UserId;
            categoryDb.Products = new List<Product>();

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
                UserId = addCategoryDto.UserId,
                Name = addCategoryDto.Name,
                Products = new List<Product>()
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
