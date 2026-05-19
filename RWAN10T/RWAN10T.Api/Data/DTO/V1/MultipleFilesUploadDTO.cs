using System.ComponentModel.DataAnnotations;

namespace RWAN10T.Api.Data.DTO.V1
{
    public class MultipleFilesUploadDTO
    {
        [Required]
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}
