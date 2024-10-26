using DropShipping.Dto.CategoryDtos;
using DropShipping.Dto.ProductDtos;
using DropShipping.Services.Implementations;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.CustomExceptions.CategoryCustromException;
using DropShipping.Shared.CustomExceptions.ProductCutsomException;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DropShipping.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<CategoryDto>> GetAllCategories()
        {
            try
            {
                string name = User.Identity.Name;
                string userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var userId = User.FindFirstValue("userId");

                return Ok(_productService.GetAllProducts(int.Parse(userId)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProductById(int id)
        {
            try
            {
                var category = _productService.GetProductById(id);
                return Ok(category);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, contact admin!");
            }
        }

        [HttpPost("addProduct")]
        public IActionResult AddProduct([FromBody] AddProductDto addProductDto)
        {
            try
            {
                _productService.AddProduct(addProductDto);
                return StatusCode(StatusCodes.Status201Created, "Products was created");
            }
            catch (DataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, contact admin!");
            }
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromBody] UpdateProductDto updateProductDto)
        {
            try
            {
                _productService.UpdateProduct(updateProductDto);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (DataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, contact admin!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return Ok($"Product with ID: {id} was successfully deleted");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, contact admin!");
            }
        }
    }
}
