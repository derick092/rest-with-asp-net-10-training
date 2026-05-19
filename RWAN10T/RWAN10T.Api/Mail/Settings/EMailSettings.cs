namespace RWAN10T.Api.Mail.Settings
{
    public class EMailSettings
    {
        public string Host { get; set; } = String.Empty;
        public int Port { get; set; } = 0;
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string From { get; set; } = String.Empty;
        public string Subject { get; set; } = String.Empty;
        public string Message { get; set; } = String.Empty;
        public bool Ssl { get; set; }

        public MailSettings Properties { get; set; } = new MailSettings();
    }
}
