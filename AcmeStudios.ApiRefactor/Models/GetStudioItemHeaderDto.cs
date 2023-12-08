using System.ComponentModel.DataAnnotations;

namespace AcemStudios.ApiRefactor.DTOs
{
    public class GetStudioItemHeaderDto
    {
        public int StudioItemId { get; set; }      
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }        

        
    }
}
