using System;
using System.ComponentModel.DataAnnotations;

namespace AcemStudios.ApiRefactor.DTOs
{
    public class GetStudioItemDto
    {
        public int StudioItemId { get; set; }
        public DateTime Acquired { get; set; }
        public DateTime? Sold { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        public decimal Price { get; set; }
        public decimal? SoldFor { get; set; }
        public bool Eurorack { get; set; }
        public int StudioItemTypeId { get; set; }
        public StudioItemType StudioItemType { get; set; }

        //public StudioItemImage StudioItemImage { get; set; }
    }
}
