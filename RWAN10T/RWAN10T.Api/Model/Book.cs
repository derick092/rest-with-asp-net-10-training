using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RWAN10T.Api.Model
{
    [Table("books")]
    public class Book
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        [Column("title", TypeName = "varchar(200)")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [Column("author", TypeName = "varchar(250)")]
        public string Author { get; set; } = string.Empty;
        [Required]
        [Column("price")]
        public decimal Price { get; set; }
        [Required]
        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }
    }
}
