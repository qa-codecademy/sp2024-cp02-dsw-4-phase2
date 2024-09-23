using DropShipping.Dto.OrderDtos;
using DropShipping.Services.Interfaces;
using DropShipping.Shared.CustomExceptions.CategoryCustromException;
using DropShipping.Shared.CustomExceptions.OrderCustomException;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DropShipping.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderService : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderService(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<List<OrderDto>> GetAllOrders()
        {
            try
            {
                string name = User.Identity.Name;
                string username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var userId = User.FindFirstValue("userId");

                return Ok(_orderService.GetAllOrders(int.Parse(userId)));
            }
            catch (Exception ex)
            {
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
            catch (OrderNotFoundException ex)
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
            catch (OrderDataException ex)
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
            catch (OrderNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (OrderDataException ex)
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
            catch (OrderNotFoundException ex)
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
