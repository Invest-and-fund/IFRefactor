using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeStudios.ApiRefactor.Domain
{
    public sealed class StudioItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudioItemId { get; init; }
        public DateTime Acquired { get; init; }
        public DateTime? Sold { get; init; }
        [Required]
        public string Name { get; init; } = string.Empty;
        [Required]
        public string Description { get; init; } = string.Empty;
        [Required] public string SerialNumber { get; init; } = string.Empty; 
        public decimal Price { get; init; }
        public decimal? SoldFor { get; init; }
        public bool Eurorack { get; init; }
        [Required]
        public int StudioItemTypeId { get; init; }
        public StudioItemType? StudioItemType { get; init; }
    }
}
