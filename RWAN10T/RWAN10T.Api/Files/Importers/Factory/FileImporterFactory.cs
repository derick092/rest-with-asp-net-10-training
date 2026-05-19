using RWAN10T.Api.Files.Importers.Contract;
using RWAN10T.Api.Files.Importers.Impl;

namespace RWAN10T.Api.Files.Importers.Factory
{
    public class FileImporterFactory(IServiceProvider serviceProvider, ILogger<FileImporterFactory> logger)
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly ILogger<FileImporterFactory> _logger = logger;

        public IFileImporter GetImporter(string fileName) 
        {
            if(fileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("Creating CSV file importer for file: {FileName}", fileName);
                return _serviceProvider.GetRequiredService<CsvFileImporter>();
            }
            else if(fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("Creating Excel file importer for file: {FileName}", fileName);
                return _serviceProvider.GetRequiredService<ExcelFileImporter>();
            }
            else
            {
                _logger.LogError("Unsupported file type: {FileName}", fileName);
                throw new NotSupportedException($"Unsupported file type: {fileName}");
            }
        }
    }
}
