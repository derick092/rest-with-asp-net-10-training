using RWAN10T.Api.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RWAN10T.Api.Model
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Column("user_name")]
        public string Username { get; set; } = String.Empty;
        [Column("full_name")]
        public string Fullname { get; set; } = String.Empty;
        [Column("password")]
        public string Password { get; set; } = String.Empty;
        [Column("refresh_token")]
        public string? RefreshToken { get; set; }
        [Column("refresh_token_expiry_time")]
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
