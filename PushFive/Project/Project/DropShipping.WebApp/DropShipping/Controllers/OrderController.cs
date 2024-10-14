using DropShipping.Dto.OrderDtos;
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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<List<OrderDto>> GetAllOrders()
        {
            try
            {
                var name = User.Identity?.Name;
                var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                var userId = User.FindFirstValue("userId");

                return Ok(_orderService.GetAllOrders(int.Parse(userId)));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXCEPTION: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, contact admin!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<OrderDto> GetOrderById(int id)
        {
            try
            {
                var order = _orderService.GetOrderById(id);
                return Ok(order);
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

        [HttpPost("addOrder")]
        public IActionResult AddOrder([FromBody] AddOrderDto addOrderDto)
        {
            try
            {
                _orderService.AddOrder(addOrderDto);
                return StatusCode(StatusCodes.Status201Created, "The order was created");
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
        public IActionResult UpdateOrder([FromBody] UpdateOrderDto updateOrderDto)
        {
            try
            {
                _orderService.UpdateOrder(updateOrderDto);
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
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _orderService.DeleteOrder(id);
                return Ok($"Order with ID: {id} was successfully deleted");
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
    }
}

//eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im5ld0FkZCIsInVzZXJGdWxsTmFtZSI6Im5ld0FkZCBuZXdBZGQiLCJTa2FmYW5kZXIiOiI3IiwibmJmIjoxNzI4NDI4ODE1LCJleHAiOjE3Mjg0MzI0MTUsImlhdCI6MTcyODQyODgxNX0.h0g_ovtdbQZBqbNVd_mJX2uJZaw19yrgdpKI - wGgtgE
//eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InRlc3QzIiwidXNlckZ1bGxOYW1lIjoidGVzdDEgdGVzdDIiLCJ1c2VySWQiOiI2IiwibmJmIjoxNzI4NDE0ODE2LCJleHAiOjE3Mjg0MTg0MTUsImlhdCI6MTcyODQxNDgxNn0.KKCu7cq9Z2KmnR24mxT_gl_Vp0hgk9RLPNSTyp2dc6w