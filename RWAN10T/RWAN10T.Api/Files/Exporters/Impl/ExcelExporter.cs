using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Files.Exporters.Contract;
using RWAN10T.Api.Files.Importers.Factory;

namespace RWAN10T.Api.Files.Exporters.Impl
{
    public class ExcelExporter : IFileExporter
    {
        public FileContentResult ExportFile(List<PersonDTO> list)
        {
            using var woorkbook = new XLWorkbook();
            var worksheet = woorkbook.Worksheets.Add("people");

            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "First Name";
            worksheet.Cell(1, 3).Value = "Last Name";
            worksheet.Cell(1, 4).Value = "Address";
            worksheet.Cell(1, 5).Value = "Gender";
            worksheet.Cell(1, 6).Value = "Enabled";

            var headerRange = worksheet.Range(1, 1, 1, 6);
            headerRange.Style.Font.Bold = true;

            int row = 2;
            foreach (var person in list)
            {
                worksheet.Cell(row, 1).Value = person.Id;
                worksheet.Cell(row, 2).Value = person.FirstName;
                worksheet.Cell(row, 3).Value = person.LastName;
                worksheet.Cell(row, 4).Value = person.Address;
                worksheet.Cell(row, 5).Value = person.Gender;
                worksheet.Cell(row, 6).Value = person.Enable == true ? "yes" : "no";
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            woorkbook.SaveAs(stream);

            var fileBytes = stream.ToArray();

            return new FileContentResult(fileBytes, MediaTypes.EXCEL)
            {
                FileDownloadName = $"people_exported_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
            };
        }
    }
}
