using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AcmeStudios.ApiRefactor.Domain
{
    public sealed class StudioItemType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudioItemTypeId { get; init; }
        [Required]
        public string Value { get; init; } = string.Empty;
        [JsonIgnore]
        public ICollection<StudioItem> StudioItem { get; init; } = new List<StudioItem>();
    }
}
