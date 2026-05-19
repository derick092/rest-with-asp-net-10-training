using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using RWAN10T.Api.Mail.Settings;

namespace RWAN10T.Api.Mail
{
    public class EmailSender(EMailSettings emailSettings, ILogger<EmailSender> logger)
    {
        private readonly EMailSettings _emailSettings = emailSettings;
        private readonly ILogger<EmailSender> _logger = logger;

        private string _to = "";
        private string _subject = "";
        private string _body = "";
        private readonly List<MailboxAddress> _recepients = new();
        private string _attachment = "";

        public EmailSender To(string to)
        {
            _to = to;
            _recepients.Clear();
            _recepients.AddRange(ParseRecipients(to));
            return this;
        }

        public EmailSender WithSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        public EmailSender WithMessage(string body)
        {
            _body = body;
            return this;
        }

        public EmailSender Attachment(string filePath)
        {
            if (File.Exists(filePath))
            {
                _attachment = filePath;
            }
            else 
            {
                _logger.LogWarning("Attachment file not found: {FilePath}", filePath);
            }
            
            return this;
        }

        public void Send() 
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.From, _emailSettings.Username));
            message.To.AddRange(_recepients);
            message.Subject = _subject ?? _emailSettings.Subject ?? "No Subject";

            var builder = new BodyBuilder { TextBody = _body ?? _emailSettings.Message ?? string.Empty };

            if (!string.IsNullOrWhiteSpace(_attachment)) 
            {
                var fileName = Path.GetFileName(_attachment);
                builder.Attachments.Add(fileName, File.ReadAllBytes(_attachment));
            }

            message.Body = builder.ToMessageBody();

            try
            {
                using var client = new SmtpClient();

                client.Connect(_emailSettings.Host, _emailSettings.Port, _emailSettings.Ssl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);

                client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                client.Send(message);
                client.Disconnect(true);

                _logger.LogInformation("Email sent successfully to {Recipients}", String.Join(";", _recepients));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Recipients}", String.Join(";", _recepients));
                throw;
            }
            finally
            {
                Reset();
            }
        }

        private IEnumerable<MailboxAddress> ParseRecipients(string to)
        {
            var toWithoutSpaces = to.Replace(" ", String.Empty);
            var recepients = toWithoutSpaces.Split(';', StringSplitOptions.RemoveEmptyEntries);

            var list = new List<MailboxAddress>();

            foreach (var receipient in recepients) 
            {
                try
                {
                    var mailbox = MailboxAddress.Parse(receipient);
                    list.Add(mailbox);
                }
                catch (FormatException ex)
                {
                    _logger.LogWarning(ex, "Invalid email address format: {EmailAddress}", receipient);
                }
            }

            return list;
        }

        private void Reset()
        {
            _to = "";
            _subject = "";
            _body = "";
            _attachment = "";
            _recepients.Clear();
        }
    }
}
