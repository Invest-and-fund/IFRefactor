using System.ComponentModel.DataAnnotations;

namespace AcmeStudios.ApiRefactor.Application.DTOs
{
    public class UpdateStudioItemDto
    {
        public int StudioItemId { get; init; }
        public DateTime Acquired { get; init; } = new DateTime(2020, 08, 04);
        public DateTime? Sold { get; init; } = null;
        [Required]
        public string Name { get; init; } = "DSI Mopho x4";
        [Required]
        public string Description { get; init; } = "Dave Smith Instruments analog poly";
        [Required]
        public string SerialNumber { get; init; } = "123456";
        public decimal Price { get; init; } = 10.00M;
        public decimal SoldFor { get; init; } = 0M;
        public bool Eurorack { get; init; } = false;
        public StudioItemTypeDto StudioItemType { get; init; }

        //public StudioItemImage StudioItemImage { get; set; }
    }
}
