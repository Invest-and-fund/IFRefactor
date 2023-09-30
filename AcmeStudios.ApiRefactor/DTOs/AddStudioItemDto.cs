using System;
using System.ComponentModel.DataAnnotations;

namespace AcmeStudios.ApiRefactor.DTOs
{
    public class AddStudioItemDto
    {
        public DateTime Acquired { get; init; }
        public DateTime? Sold { get; init; } = null;
        [Required]
        public string Name { get; init; }
        [Required]
        public string Description { get; init; }
        [Required]
        public string SerialNumber { get; init; }
        public decimal Price { get; init; } = 10.00M;
        public decimal SoldFor { get; init; } = 0M;
        public bool Eurorack { get; init; } = false;
        public int StudioItemTypeId { get; init; }

        //public StudioItemImage StudioItemImage { get; set; }
    }
}
