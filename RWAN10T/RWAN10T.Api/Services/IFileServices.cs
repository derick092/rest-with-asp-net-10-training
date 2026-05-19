using RWAN10T.Api.Data.DTO.V1;

namespace RWAN10T.Api.Services
{
    public interface IFileServices
    {
        byte[] GetFile(string fileName);
        Task<FileDetailDTO> SaveFileToDisk(IFormFile file);
        Task<List<FileDetailDTO>> SaveMultipleFileToDisk(List<IFormFile> files);
    }
}
