using Microsoft.AspNetCore.Mvc;
using RWAN10T.Api.Data.DTO.V1;

namespace RWAN10T.Api.Files.Exporters.Contract
{
    public interface IFileExporter
    {
        FileContentResult ExportFile(List<PersonDTO> list);
    }
}
