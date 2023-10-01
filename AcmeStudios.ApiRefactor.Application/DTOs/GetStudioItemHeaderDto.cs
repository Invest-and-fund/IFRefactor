using System.ComponentModel.DataAnnotations;

namespace AcmeStudios.ApiRefactor.Application.DTOs
{
    public sealed class GetStudioItemHeaderDto
    {
        public GetStudioItemHeaderDto(int studioItemId,
            string name,
            string description)
        {
            StudioItemId = studioItemId;
            Name = name;
            Description = description;
        }

        public int StudioItemId { get; init; }      
        [Required]
        public string Name { get; init; }
        [Required]
        public string Description { get; init; }           
    }
}
