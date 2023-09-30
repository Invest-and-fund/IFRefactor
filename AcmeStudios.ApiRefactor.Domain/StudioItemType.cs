using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AcmeStudios.ApiRefactor.Domain
{
    public class StudioItemType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudioItemTypeId { get; init; }
        [Required]
        public string Value { get; init; }
        [JsonIgnore]
        public ICollection<StudioItem> StudioItem { get; init; }
    }
}
