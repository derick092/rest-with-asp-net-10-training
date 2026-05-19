using RWAN10T.Api.Data.DTO.V1;

namespace RWAN10T.Api.Files.Importers.Contract
{
    public interface IFileImporter
    {
        Task<List<PersonDTO>> ImportFileAsync(Stream fileStream);
    }
}
