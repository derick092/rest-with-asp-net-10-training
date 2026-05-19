using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Files.Exporters.Contract;
using RWAN10T.Api.Files.Importers.Factory;
using System.Globalization;
using System.Text;

namespace RWAN10T.Api.Files.Exporters.Impl
{
    public class CsvExporter : IFileExporter
    {
        public FileContentResult ExportFile(List<PersonDTO> list)
        {
            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream, Encoding.UTF8);

            using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            });

            csv.WriteRecords(list);
            writer.Flush();

            var fileBytes = memoryStream.ToArray();

            return new FileContentResult(fileBytes, MediaTypes.CSV)
            {
                FileDownloadName = $"people_exported_{DateTime.UtcNow:yyyyMMddHHmmss}.csv"
            };
        }
    }
}
