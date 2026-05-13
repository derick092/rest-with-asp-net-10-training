using RWAN10T.Api.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RWAN10T.Api.Data.DTO.V1
{
    public class BookDTO
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; }
    }
}
