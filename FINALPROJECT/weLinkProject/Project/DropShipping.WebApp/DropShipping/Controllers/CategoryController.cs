using DropShipping.Dto.CategoryDtos;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DropShipping.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAllCategories")]
        public ActionResult<List<CategoryDto>> GetAllCategories()
        {
            try
            {
                string name = User.Identity?.Name;
                string userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                var userId = User.FindFirstValue("userId");


                return Ok(_categoryService.GetAllCategories());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, contact admin!");
            }
        }

        [HttpGet("GetCategoryById/{id}")]
        public ActionResult<CategoryDto> GetCategoryById(int id)
        {
            try
            {
                var category = _categoryService.GetCategoryById(id);
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

        [HttpPost("AddCategory")]
        public IActionResult AddCategory([FromBody] AddCategoryDto addCategoryDto)
        {
            try
            {
                _categoryService.AddCategory(addCategoryDto);
                return StatusCode(StatusCodes.Status201Created, "Category was created");
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

        [HttpPut("UpdateCategory")]
        public IActionResult UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                _categoryService.UpdateCategory(updateCategoryDto);
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

        [HttpDelete("DeleteCategoryById/")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return Ok($"Category with ID: {id} was successfully deleted");
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
