using RWAN10T.Api.Data.DTO.V1;

namespace RWAN10T.Api.Services
{
    public interface IEmailService
    {
        void SendSimpleEmail(EmailRequestDTO emailRequest);
        void SendEmailWithAttachment(EmailRequestDTO emailRequest, IFormFile attachment);
    }
}
