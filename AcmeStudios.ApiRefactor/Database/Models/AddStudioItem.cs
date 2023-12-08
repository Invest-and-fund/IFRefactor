using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AcemStudios.ApiRefactor.Database.Models
{
    public class StudioItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudioItemId { get; set; }
        public DateTime Acquired { get; set; }
        public DateTime? Sold { get; set; } = null;
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        public decimal Price { get; set; } //= 10.00M;
        public decimal? SoldFor { get; set; } //= 0M;
        public bool Eurorack { get; set; } //= false;
        [Required]
        public int StudioItemTypeId { get; set; }
        public StudioItemType StudioItemType { get; set; }
    }

    public class StudioItemType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudioItemTypeId { get; set; }
        [Required]
        public string Value { get; set; }
        [JsonIgnore]
        public ICollection<StudioItem> StudioItem { get; set; }
    }
}
