using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeStudios.ApiRefactor.Domain
{
    public sealed class StudioItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudioItemId { get; init; }
        public DateTime Acquired { get; init; }
        public DateTime? Sold { get; init; } = null;
        [Required]
        public string Name { get; init; }
        [Required]
        public string Description { get; init; }
        [Required]
        public string SerialNumber { get; init; }
        public decimal Price { get; init; } //= 10.00M;
        public decimal? SoldFor { get; init; } //= 0M;
        public bool Eurorack { get; init; } //= false;
        [Required]
        public int StudioItemTypeId { get; init; }
        public StudioItemType StudioItemType { get; init; }
    }
}
