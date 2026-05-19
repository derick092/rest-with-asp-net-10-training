using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RWAN10T.Api.Controllers.V1
{
    [Route("api/[controller]/v1")]
    [ApiController]
    [Authorize("Bearer")]
    public class EmailController : ControllerBase
    {
        private IEmailService _emailService;
        private readonly ILogger<PersonController> _logger;

        public EmailController(IEmailService emailService, ILogger<PersonController> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost("sendMail")]
        public IActionResult SendEmail([FromBody] EmailRequestDTO emailRequest)
        {
            _logger.LogInformation("Sending email to: {to}", emailRequest.To);

            if (emailRequest == null)
            {
                _logger.LogWarning("Failed to send email: Request is null");
                return BadRequest(); 
            }

            _emailService.SendSimpleEmail(emailRequest);

            return Ok();
        }

        [HttpPost("sendMailWithAttachment")]
        public IActionResult SendEmailWithAttachment([FromBody] EmailRequestDTO emailRequest, [FromForm] FileUploadDTO file)
        {
            _logger.LogInformation("Sending email with attachment to: {to}", emailRequest.To);
            if (emailRequest == null)
            {
                _logger.LogWarning("Failed to send email with attachment: Request is null");
                return BadRequest();
            }
            if (file.File == null || file.File.Length == 0)
            {
                _logger.LogWarning("Failed to send email with attachment: No attachment provided");
                return BadRequest("Attachment is required.");
            }
            _emailService.SendEmailWithAttachment(emailRequest, file.File);

            return Ok();
        }
    }
}
