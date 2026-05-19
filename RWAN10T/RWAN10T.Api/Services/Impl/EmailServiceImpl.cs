using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Mail;

namespace RWAN10T.Api.Services.Impl
{
    public class EmailServiceImpl(EmailSender emailSender, Logger<EmailServiceImpl> logger) : IEmailService
    {
        private readonly EmailSender _emailSender = emailSender;
        private readonly Logger<EmailServiceImpl> _logger = logger;

        public void SendSimpleEmail(EmailRequestDTO emailRequest)
        {
            _emailSender.To(emailRequest.To)
                        .WithSubject(emailRequest.Subject)
                        .WithMessage(emailRequest.Body)
                        .Send();
        }

        public async void SendEmailWithAttachment(EmailRequestDTO emailRequest, IFormFile attachment)
        {
            if (attachment == null || attachment.Length == 0) 
            {
                _logger.LogWarning("No attachment provided for email to {To}", emailRequest.To);
                throw new ArgumentException("Attachment is required for this method.");
            }

            string tempFilePath = Path.Combine(Path.GetTempPath(), attachment.FileName);

            try
            {
                using var stream = new FileStream(tempFilePath, FileMode.Create);
                await attachment.CopyToAsync(stream);

                _emailSender.To(emailRequest.To)
                            .WithSubject(emailRequest.Subject)
                            .WithMessage(emailRequest.Body)
                            .Attachment(tempFilePath)
                            .Send();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {To} with attachment {FileName}", emailRequest.To, attachment.FileName);
                throw;
            }
            finally 
            {
                if (File.Exists(tempFilePath)) File.Delete(tempFilePath);
            }
        }
    }
}
