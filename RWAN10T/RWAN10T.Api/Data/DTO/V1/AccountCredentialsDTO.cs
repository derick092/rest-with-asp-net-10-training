namespace RWAN10T.Api.Data.DTO.V1
{
    public class AccountCredentialsDTO
    {
        public AccountCredentialsDTO()
        {
            
        }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
    }
}
