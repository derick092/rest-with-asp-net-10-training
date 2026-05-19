using RWAN10T.Api.Data.DTO.V1;

namespace RWAN10T.Api.Services.Impl
{
    public class FileServicesImpl : IFileServices
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _contextAccessor;
        private static readonly HashSet<string> _allowedExtensions = new() { ".txt", ".pdf", ".docx", ".xlsx", ".png", ".jpg", ".jpeg" };

        public FileServicesImpl(HttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _basePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadDir");

            if(!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }

        }

        public byte[] GetFile(string fileName)
        {
            var filePath = Path.Combine(_basePath, fileName);
            if (!File.Exists(filePath)) return null;
            return File.ReadAllBytes(filePath);
        }

        public async Task<FileDetailDTO> SaveFileToDisk(IFormFile file)
        {
            if (file == null || file.Length == 0) 
            {
                throw new ArgumentException("File is null or empty.");
            }

            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if(!_allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException($"File type {fileExtension} is not allowed.");
            }

            var docName = Path.GetFileName(file.FileName);
            var destination = Path.Combine(_basePath, docName);

            var baseUrl = $"{_contextAccessor?.HttpContext?.Request.Scheme}://{_contextAccessor?.HttpContext?.Request.Host}";

            var fileDetail = new FileDetailDTO
            {
                DocumentName = docName,
                DocType = file.ContentType,
                DocUrl = $"{baseUrl}/api/file/v1/downloadFile/{docName}"
            };

            using var stream = new FileStream(destination, FileMode.Create);
            await file.CopyToAsync(stream);

            return fileDetail;
        }

        public async Task<List<FileDetailDTO>> SaveMultipleFileToDisk(List<IFormFile> files)
        {
            var result = new List<FileDetailDTO>();

            foreach (var file in files)
            {
                result.Add(await SaveFileToDisk(file));
            }

            return result;
        }
    }
}
