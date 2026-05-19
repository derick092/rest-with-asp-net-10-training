using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Services;

namespace RWAN10T.Api.Controllers.V1
{
    [Route("api/[controller]/v1")]
    [ApiController]
    [Authorize("Bearer")]
    public class FileController(IFileServices fileServices, ILogger<FileController> logger) : ControllerBase
    {
        private IFileServices _fileServices = fileServices;
        private readonly ILogger<FileController> _logger = logger;

        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile([FromForm]FileUploadDTO file)
        {
            var fileDetail = await _fileServices.SaveFileToDisk(file.File);
            return Ok(fileDetail);
        }

        [HttpPost("uploadFiles")]
        public async Task<IActionResult> UploadFiles([FromForm] MultipleFilesUploadDTO files) 
        {
            var filesDetail = await _fileServices.SaveMultipleFileToDisk(files.Files);
            return Ok(filesDetail);
        }

        [HttpGet("downloadFile/{fileName}")]
        public IActionResult DownloadFile(string fileName) 
        {
            var buffer = _fileServices.GetFile(fileName);
            if (buffer == null || buffer.Length == 0) return NoContent();

            var contentType = $"application/{Path.GetExtension(fileName).TrimStart('.').ToLower()}";
            return File(buffer, contentType, fileName);
        }
    }
}
