using DropShipping.Dto.ProductDtos;

namespace DropShipping.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetAllProducts(int userId);

        ProductDto GetProductById(int id);

        void AddProduct(AddProductDto product);

        void UpdateProduct(UpdateProductDto product);

        void DeleteProduct(int id);
    }
}
