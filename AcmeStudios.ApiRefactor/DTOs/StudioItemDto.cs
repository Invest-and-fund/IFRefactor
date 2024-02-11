using System;

using AcmeStudios.ApiRefactor.Entities;

namespace AcmeStudios.ApiRefactor.DTOs
{
    public record StudioItemDto
    {
        public StudioItemDto()
        {
        }

        public int StudioItemId { get; init; }
        public DateTime Acquired { get; init; }
        public DateTime? Sold { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string SerialNumber { get; init; }
        public decimal Price { get; init; }
        public decimal? SoldFor { get; init; }
        public bool Eurorack { get; init; }

        public int StudioItemTypeId { get; init; }
        public StudioItemType StudioItemType { get; init; }
    }
}
