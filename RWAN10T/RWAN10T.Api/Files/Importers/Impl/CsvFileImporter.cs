using CsvHelper;
using CsvHelper.Configuration;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Files.Importers.Contract;
using System.Globalization;

namespace RWAN10T.Api.Files.Importers.Impl
{
    public class CsvFileImporter : IFileImporter
    {
        public async Task<List<PersonDTO>> ImportFileAsync(Stream fileStream)
        {
            using var reader = new StreamReader(fileStream);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true
            });

            var result = new List<PersonDTO>();

            await foreach (var record in csv.GetRecordsAsync<dynamic>())
            {
                var person = new PersonDTO
                {
                    FirstName = record.first_name,
                    LastName = record.last_name,
                    Address = record.address,
                    Gender = record.gender,
                    Enable = true
                };

                result.Add(person);
            }

            return result;
        }
    }
}
