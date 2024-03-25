using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AcmeStudios.ApiRefactor.Interfaces;
using Newtonsoft.Json;

namespace AcmeStudios.ApiRefactor.Entities;

public class StudioItemType: IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required]
    public string Value { get; set; }
    [JsonIgnore]
    public ICollection<StudioItem> StudioItem { get; set; }

    
}