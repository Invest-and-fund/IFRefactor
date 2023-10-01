using System.ComponentModel.DataAnnotations;

namespace AcmeStudios.ApiRefactor.Application.DTOs
{
    public sealed class GetStudioItemDto
    {
        public GetStudioItemDto(int studioItemId,
            DateTime acquired,
            DateTime? sold,
            string name,
            string description,
            string serialNumber,
            decimal price,
            decimal? soldFor,
            bool eurorack,
            int studioItemTypeId,
            StudioItemTypeDto studioItemType)
        {
            StudioItemId = studioItemId;
            Acquired = acquired;
            Sold = sold;
            Name = name;
            Description = description;
            SerialNumber = serialNumber;
            Price = price;
            SoldFor = soldFor;
            Eurorack = eurorack;
            StudioItemTypeId = studioItemTypeId;
            StudioItemType = studioItemType;
        }

        public int StudioItemId { get; init; }
        public DateTime Acquired { get; init; }
        public DateTime? Sold { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string Description { get; init; }
        [Required]
        public string SerialNumber { get; init; }
        public decimal Price { get; init; }
        public decimal? SoldFor { get; init; }
        public bool Eurorack { get; init; }
        public int StudioItemTypeId { get; init; }
        public StudioItemTypeDto StudioItemType { get; init; }
    }
}
