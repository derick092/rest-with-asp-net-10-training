namespace RWAN10T.Api.Data.DTO.V1
{
    public class TokenDTO
    {
        public TokenDTO()
        {
        }

        public TokenDTO(bool authenticated, string created, string expiration, string accesToken, string refreshToken)
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            AccesToken = accesToken;
            RefreshToken = refreshToken;
        }

        public bool Authenticated { get; set; } = false;
        public string Created { get; set; } = string.Empty;
        public string Expiration { get; set; } = string.Empty;
        public string AccesToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
