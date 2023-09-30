using System;
using System.ComponentModel.DataAnnotations;

namespace AcmeStudios.ApiRefactor.DTOs
{
    public class GetStudioItemDto
    {
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

        //public StudioItemImage StudioItemImage { get; set; }
    }
}
