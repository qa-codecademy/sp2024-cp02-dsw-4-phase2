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

        [HttpGet("GetAllOrders")]
        public ActionResult<List<OrderDto>> GetAllOrders()
        {
            try
            {
                var name = User.Identity?.Name;
                var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                var userId = User.FindFirstValue("userId");

                return Ok(_orderService.GetAllOrders(int.Parse(userId)));
            }
            catch(NoDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, contact admin!");
            }
        }

        [HttpGet("GetOrderById/{id}")]
        public ActionResult<OrderDto> GetOrderById(int id)
        {
            try
            {
                var userId = User.FindFirstValue("userId");
                var order = _orderService.GetOrderById(id, int.Parse(userId));

                if (order.UserId != int.Parse(userId))
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have access to this order.");
                }

                return Ok(order);
            }
            catch (NotFoundException ex)
            {
                return NotFound("The order was not found.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ("You do not have access to this order."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occurred, contact admin!");
            }
        }

        [HttpPost("AddOrder")]
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

        //[HttpPut("UpdateOrder")]
        //public IActionResult UpdateOrder([FromBody] UpdateOrderDto updateOrderDto)
        //{
        //    try
        //    {
        //        _orderService.UpdateOrder(updateOrderDto);
        //        return NoContent();
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        //    }
        //    catch (NoDataException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "System error occured, contact admin!");
        //    }
        //}

        [HttpPut("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] UpdateOrderDto updateOrderDto)
        {
            try
            {
                var userIdString = User.FindFirstValue("userId");

                if (string.IsNullOrEmpty(userIdString))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "User is not authenticated.");
                }

                var userId = int.Parse(userIdString);

                _orderService.UpdateOrder(updateOrderDto, userId);

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occurred, contact admin!");
            }
        }

        [HttpDelete("DeleteOrderById/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                var userIdString = User.FindFirstValue("userId");

                if (string.IsNullOrEmpty(userIdString))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "User is not authenticated.");
                }

                var userId = int.Parse(userIdString);

                _orderService.GetOrderById(id, userId);

                _orderService.DeleteOrder(id);

                return Ok($"Order with ID: {id} was successfully deleted");
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occurred, contact admin!");
            }
        }
    }
}