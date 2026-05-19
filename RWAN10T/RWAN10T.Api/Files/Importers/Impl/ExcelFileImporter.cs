using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Files.Importers.Contract;
using System.Globalization;

namespace RWAN10T.Api.Files.Importers.Impl
{
    public class ExcelFileImporter : IFileImporter
    {
        public async Task<List<PersonDTO>> ImportFileAsync(Stream fileStream)
        {
            var results = new List<PersonDTO>();
            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheets.First();

            var rows = worksheet.RowsUsed().Skip(1); // Skip header row

            foreach (var row in rows) 
            {
                if (!row.Cell(1).IsEmpty()) 
                {
                    results.Add(new PersonDTO
                    {
                        FirstName = row.Cell(1).GetString(),
                        LastName = row.Cell(2).GetString(),
                        Address = row.Cell(3).GetString(),
                        Gender = row.Cell(4).GetString(),
                        Enable = true
                    });
                }
            }

            return await Task.FromResult(results);
        }
    }
}
