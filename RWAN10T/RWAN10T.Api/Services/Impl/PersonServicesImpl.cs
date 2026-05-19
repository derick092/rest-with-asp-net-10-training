using Mapster;
using Microsoft.AspNetCore.Mvc;
using RWAN10T.Api.Data.Converter.Impl;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Files.Exporters.Factory;
using RWAN10T.Api.Files.Importers.Factory;
using RWAN10T.Api.Hypermdia.Utils;
using RWAN10T.Api.Model;
using RWAN10T.Api.Model.Context;
using RWAN10T.Api.Repositories;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RWAN10T.Api.Services.Impl
{
    public class PersonServicesImpl : IPersonServices
    {
        private IPersonRepository _repository;
        private readonly PersonConverter _converter;
        private readonly FileImporterFactory _fileImporterFactory;
        private readonly FileExporterFactory _fileExporterFactory;
        private readonly ILogger<PersonServicesImpl> _logger;

        public PersonServicesImpl(IPersonRepository repository, FileImporterFactory fileImporterFactory, FileExporterFactory fileExporterFactory, ILogger<PersonServicesImpl> logger)
        {
            _repository = repository;
            _converter = new PersonConverter();
            _fileImporterFactory = fileImporterFactory;
            _fileExporterFactory = fileExporterFactory;
            _logger = logger;
        }

        public PersonDTO Create(PersonDTO person)
        {
            return _converter.Parse(_repository.Create(_converter.Parse(person)!))!;
        }
        public PersonDTO? Update(PersonDTO person)
        {
            return _converter.Parse(_repository.Update(_converter.Parse(person)!)!);
        }

        public void Delete(long id)
        {
           _repository.Delete(id);
        }
        public PersonDTO? FindById(long id)
        {
           return _converter.Parse(_repository.FindById(id)!);
        }

        public List<PersonDTO>? FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public PersonDTO? Disable(long id)
        {
            if (_repository != null)
            {
                var person = _repository.Disable(id);
                return _converter.Parse(person);
            }
            return null;
        }

        public List<PersonDTO> FindByName(string firstName, string lastName)
        {
            return _repository.FindByName(firstName, lastName).Adapt<List<PersonDTO>>();
        }

        public PagedSearchDTO<PersonDTO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var result = _repository.FindWithPagedSearch(name, sortDirection, pageSize, page);

            return new PagedSearchDTO<PersonDTO> 
            {
                CurrentPage = page,
                List = result.List.Adapt<List<PersonDTO>>(),
                PageSize = result.PageSize,
                SortDirection = result.SortDirection,
                TotalResults = result.TotalResults
            };
        }

        public async Task<List<PersonDTO>> MassCreationAsync(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                _logger.LogWarning("No file provided for mass creation.");
                throw new ArgumentException("File is required for mass creation.");
            }

            using var stream = file.OpenReadStream();
            var fileName = file.FileName;

            try
            {
                var importer = _fileImporterFactory.GetImporter(fileName);
                var result = await importer.ImportFileAsync(stream);

                var entities = result.Select(dto => _repository.Create(dto.Adapt<Person>())).ToList();

                return entities.Adapt<List<PersonDTO>>();
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred during mass creation from file: {FileName}", fileName);
                throw;
            }
        }

        public FileContentResult ExportPage(int page, int pageSize, string sortDirection, string acceptHeader, string name)
        {
            var content = _repository.FindWithPagedSearch(name, sortDirection, pageSize, page);
            _logger.LogInformation("Exporting page {Page} with page size {PageSize} and sort direction {SortDirection} for name filter {Name}", page, pageSize, sortDirection, name);

            var exporter = _fileExporterFactory.GetExporter(acceptHeader);
            var result = content.List.Adapt<List<PersonDTO>>();

            return exporter.ExportFile(result);
        }
    }
}
