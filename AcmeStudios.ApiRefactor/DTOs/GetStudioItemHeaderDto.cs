using System.ComponentModel.DataAnnotations;

namespace AcmeStudios.ApiRefactor.DTOs
{
    public class GetStudioItemHeaderDto
    {
        public long StudioItemId { get; set; }      
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }        

        
    }
}
