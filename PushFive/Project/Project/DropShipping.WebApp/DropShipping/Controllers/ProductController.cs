using DropShipping.Dto.ProductDtos;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DropShipping.Controllers
{
    [Authorize]
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
        public ActionResult<List<ProductDto>> GetAllProducts()
        {
            try
            {
                string name = User.Identity?.Name;
                string userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                var userId = User.FindFirstValue("UserId");

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
                var product = _productService.GetProductById(id);
                return Ok(product);
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
            catch (NoDataException ex)
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
            catch (NoDataException ex)
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
