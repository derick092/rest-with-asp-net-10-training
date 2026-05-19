namespace RWAN10T.Api.Data.DTO.V1
{
    public class EmailRequestDTO
    {
        public string To { get; set; } = String.Empty;
        public string Subject { get; set; } = String.Empty;
        public string Body { get; set; } = String.Empty;
    }
}
