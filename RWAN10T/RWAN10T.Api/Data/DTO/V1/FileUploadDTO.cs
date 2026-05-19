using System.ComponentModel.DataAnnotations;

namespace RWAN10T.Api.Data.DTO.V1
{
    public class FileUploadDTO
    {
        [Required]
        public IFormFile File { get; set; } = null!;
    }
}
