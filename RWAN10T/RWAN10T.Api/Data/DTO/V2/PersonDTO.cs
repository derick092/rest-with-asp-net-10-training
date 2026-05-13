using RWAN10T.Api.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RWAN10T.Api.Data.DTO.V2
{
    public class PersonDTO
    {
        public long Id { get; set; }
        //[JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;
        //[JsonPropertyName("last_name")]
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string LastName { get; set; } = string.Empty;
        //[JsonPropertyOrder(1)]
        public string Address { get; set; } = string.Empty;
        //[JsonConverter(typeof(JsonSerializer.GenderSerializer))]
        public string Gender { get; set; } = string.Empty;
        //[JsonPropertyOrder(2)]
        //[JsonConverter(typeof(JsonSerializer.DateSerializer))]
        public DateTime? BirthDay { get; set; }
    }
}
