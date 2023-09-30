using System.ComponentModel.DataAnnotations;

namespace AcmeStudios.ApiRefactor.DTOs
{
    public class GetStudioItemHeaderDto
    {
        public int StudioItemId { get; init; }      
        [Required]
        public string Name { get; init; }
        [Required]
        public string Description { get; init; }        

        
    }
}
