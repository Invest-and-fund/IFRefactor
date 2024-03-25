using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AcmeStudios.ApiRefactor.Interfaces;

namespace AcmeStudios.ApiRefactor.Entities;

public class StudioItem : IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public DateTime Acquired { get; set; }
    public DateTime? Sold { get; set; } = null;
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string SerialNumber { get; set; }
    public decimal Price { get; set; }
    public decimal? SoldFor { get; set; }
    public bool Eurorack { get; set; }
    
    [Required]
    public long StudioItemTypeId { get; set; }
    public StudioItemType StudioItemType { get; set; }

}