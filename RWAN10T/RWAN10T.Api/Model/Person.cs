using RWAN10T.Api.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RWAN10T.Api.Model
{
    [Table("person")]
    public class Person : BaseEntity
    {
        [Required]
        [Column("first_name", TypeName ="varchar(80)")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [Column("last_name", TypeName = "varchar(80)")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [Column("address", TypeName = "varchar(100)")]
        public string Address { get; set; } = string.Empty;
        [Required]
        [Column("gender", TypeName = "varchar(6)")]
        public string Gender { get; set; } = string.Empty;
    }
}
