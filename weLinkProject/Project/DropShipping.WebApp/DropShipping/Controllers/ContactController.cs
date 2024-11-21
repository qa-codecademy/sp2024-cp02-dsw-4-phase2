using DropShipping.Dto.ContactDto;
using DropShipping.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DropShipping.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost("SendOrderNotification")]
        public async Task<IActionResult> SendOrderNotification([FromBody] EmailRequestDto request)
        {
            string userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrWhiteSpace(userEmail))
            {
                return Unauthorized("Email claim not found.");
            }

            try
            {
                await _contactService.SendEmailAsync(userEmail, request.Description);
                return Ok("Order notification email sent successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send email: {ex.Message}");
            }
        }

        [HttpPost("ContactUs")]
        public async Task<IActionResult> SendContactUsEmail([FromBody] EmailRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                await _contactService.SendContactUsEmailAsync(request.Email, request.Description);
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Failed to send email.");
            }
        }

    }
}
