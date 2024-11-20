using DropShipping.Dto.CategoryDtos;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using XAct.Security;

namespace DropShipping.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<List<CategoryDto>> GetAllCategories()
        {
            try
            {
                string name = User.Identity.Name;
                string userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var userId = User.FindFirstValue("userId");

                return Ok(_categoryService.GetAllCategories(int.Parse(userId)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, contact admin!");
            }
        }

        [HttpGet("{id}")]
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

        [HttpPost("addCategory")]
        public IActionResult AddCategory([FromBody] AddCategoryDto addCategoryDto)
        {
            try
            {
                _categoryService.AddCategory(addCategoryDto);
                return StatusCode(StatusCodes.Status201Created, "Category was created");
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
