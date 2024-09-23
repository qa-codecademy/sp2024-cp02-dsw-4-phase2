using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
using DropShipping.Dto.CategoryDtos;
using DropShipping.Mappers;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.CustomExceptions.CategoryCustromException;

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
            var categoryDb = _categoryRepository.GetAll().Where(x => x.UserId == userId);
            if (categoryDb.Any())
            {
                return new List<CategoryDto>();
            }

            return categoryDb.Select(x => x.ToCategoryDto()).ToList();
        }

        public CategoryDto GetCategoryById(int id)
        {
            Category categoryDb = _categoryRepository.GetById(id);
            if (categoryDb == null)
            {
                throw new CategoryNotFoundException($"Category with id: {id} was not found!");
            }

            CategoryDto categoryDto = categoryDb.ToCategoryDto();
            return categoryDto;
        }

        public void UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            Category categoryDb = _categoryRepository.GetById(updateCategoryDto.Id);
            if (categoryDb == null)
            {
                throw new CategoryNotFoundException($"Category with ID: {updateCategoryDto.Id} was not found!");
            }

            User userDb = _userRepository.GetById(updateCategoryDto.UserId);
            if (userDb == null)
            {
                throw new CategoryNotFoundException($"User with ID: {updateCategoryDto.UserId} was not found");
            }

            if (string.IsNullOrEmpty(updateCategoryDto.Name))
            {
                throw new CategoryDataException("Name: Name is required field");
            }

            categoryDb.Name = updateCategoryDto.Name;
        }

        public void AddCategory(AddCategoryDto addCategoryDto)
        {
            User userDb = _userRepository.GetById(addCategoryDto.UserId);
            if (userDb == null)
            {
                throw new CategoryDataException($"User with ID: {addCategoryDto.UserId} does not exist!");
            }

            if (string.IsNullOrEmpty(addCategoryDto.Name))
            {
                throw new CategoryDataException("Name: Name is required field");
            }
        }

        public void DeleteCategory(int id)
        {
            Category categoryDb = _categoryRepository.GetById(id);
            if (categoryDb == null)
            {
                throw new CategoryDataException($"Order with ID: {id} was not found!");
            }

            _categoryRepository.Delete(categoryDb);
        }
    }
}
