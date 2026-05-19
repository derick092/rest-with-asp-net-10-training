using RWAN10T.Api.Files.Exporters.Contract;
using RWAN10T.Api.Files.Exporters.Impl;
using RWAN10T.Api.Files.Importers.Factory;

namespace RWAN10T.Api.Files.Exporters.Factory
{
    public class FileExporterFactory(IServiceProvider serviceProvider, ILogger<FileImporterFactory> logger)
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly ILogger<FileImporterFactory> _logger = logger;

        public IFileExporter GetExporter(string acceptHeader) 
        {
            if (string.Equals(acceptHeader, MediaTypes.EXCEL, StringComparison.OrdinalIgnoreCase)) 
            {
                return _serviceProvider.GetRequiredService<ExcelExporter>();
            }
            else if(string.Equals(acceptHeader, MediaTypes.CSV, StringComparison.OrdinalIgnoreCase)) 
            {
                return _serviceProvider.GetRequiredService<CsvExporter>();
            }
            else 
            {
                _logger.LogWarning("Unsupported media type: {AcceptHeader}", acceptHeader);
                throw new NotSupportedException($"Unsupported media type: {acceptHeader}");
            }
        }
    }
}
