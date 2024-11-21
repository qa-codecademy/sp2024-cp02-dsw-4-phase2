using DropShipping.Dto.OrderDtos;
using DropShipping.Services.Implementations;
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
        private readonly IContactService _contactService;

        public OrderController(IOrderService orderService, IContactService contactService)
        {
            _orderService = orderService;
            _contactService = contactService;
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
            catch (NoDataException ex)
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
                var userIdString = User.FindFirstValue("userId");

                if (string.IsNullOrEmpty(userIdString))
                {
                    return Unauthorized("User is not authenticated.");
                }

                var userId = int.Parse(userIdString);  // Ensure that this is correctly parsed
                Console.WriteLine("Extracted UserId from token: " + userId); // Debugging line

                addOrderDto.UserId = userId; // Set the UserId in the DTO
                Console.WriteLine("UserId in AddOrderDto: " + addOrderDto.UserId); // Debugging line

                _orderService.AddOrder(addOrderDto);

                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                if (!string.IsNullOrEmpty(userEmail))
                {
                    var emailDescription = "Thank you for your order! Your order has been confirmed and is being processed.";
                    _contactService.SendEmailAsync(userEmail, emailDescription).Wait();
                }
                return StatusCode(StatusCodes.Status201Created, new { message = "The order was created successfully." });

            }
            catch (NoDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occurred, contact admin!");
            }
        }

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

                _orderService.DeleteOrder(id, userId);

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

        [HttpDelete("RemoveProductFromOrder/{orderId}/{productId}")]
        public IActionResult RemoveProductFromOrder(int orderId, int productId)
        {
            try
            {
                var userIdString = User.FindFirstValue("userId");

                if (string.IsNullOrEmpty(userIdString))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "User is not authenticated.");
                }

                var userId = int.Parse(userIdString);

                _orderService.RemoveProductFromOrder(orderId, productId, userId);

                return Ok($"Product with ID {productId} was successfully removed from order with ID {orderId}");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error occurred, contact admin!");
            }
        }
    }
}