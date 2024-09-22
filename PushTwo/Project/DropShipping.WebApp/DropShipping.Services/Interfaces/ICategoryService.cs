using DropShipping.Dto.CategoryDtos;

namespace DropShipping.Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryDto> GetAllCategories(int userId);

        CategoryDto GetCategoryById(int id);

        void AddCategory(AddCategoryDto categoryDto);

        void UpdateCategory(UpdateCategoryDto categoryDto);

        void DeleteCategory(int id);
    }
}
