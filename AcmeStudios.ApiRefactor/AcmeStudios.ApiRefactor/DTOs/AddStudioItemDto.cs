using System;
using System.ComponentModel.DataAnnotations;

namespace AcemStudios.ApiRefactor.DTOs
{
    public class AddStudioItemDto
    {
        public DateTime Acquired { get; set; }
        public DateTime? Sold { get; set; } = null;
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        public decimal Price { get; set; } = 10.00M;
        public decimal SoldFor { get; set; } = 0M;
        public bool Eurorack { get; set; } = false;
        public int StudioItemTypeId { get; set; }

        //public StudioItemImage StudioItemImage { get; set; }
    }
}
