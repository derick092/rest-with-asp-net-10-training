using RWAN10T.Api.Data.Converter.Contract;
using RWAN10T.Api.Data.DTO.V1;
using RWAN10T.Api.Data.DTO.V2;
using RWAN10T.Api.Model;

namespace RWAN10T.Api.Data.Converter.Impl
{
    public class PersonConverter : IParser<Person, DTO.V1.PersonDTO>, IParser<DTO.V1.PersonDTO, Person>, IParser<Person, DTO.V2.PersonDTO>, IParser<DTO.V2.PersonDTO, Person>
    {
        public DTO.V1.PersonDTO? Parse(Person origin)
        {
            if (origin == null) return null;
            return new DTO.V1.PersonDTO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }
        public Person? Parse(DTO.V1.PersonDTO origin)
        {
            if (origin == null) return null;
            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public Person? Parse(DTO.V2.PersonDTO origin)
        {
            if (origin == null) return null;
            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public List<DTO.V1.PersonDTO>? ParseList(List<Person> origin)
        {
            if(origin == null) return null;
            return origin.Select(item => Parse(item)!).ToList();
        }

        public List<Person>? ParseList(List<DTO.V1.PersonDTO> origin)
        {
            if(origin == null) return null;
            return origin.Select(item => Parse(item)!).ToList();
        }

        public List<Person>? ParseList(List<DTO.V2.PersonDTO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)!).ToList();
        }

        DTO.V2.PersonDTO? IParser<Person, DTO.V2.PersonDTO>.Parse(Person origin)
        {
            if (origin == null) return null;
            return new DTO.V2.PersonDTO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender,
                BirthDay = DateTime.Now
            };
        }

        List<DTO.V2.PersonDTO>? IParser<Person, DTO.V2.PersonDTO>.ParseList(List<Person> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => ((IParser<Person, DTO.V2.PersonDTO>)this).Parse(item)!).ToList();
        }

        
    }
}
