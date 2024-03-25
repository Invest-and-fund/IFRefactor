using System.ComponentModel.DataAnnotations;

namespace AcmeStudios.ApiRefactor.DTOs
{
    public class GetStudioItemHeaderDto
    {
        public long Id { get; set; }      
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }        

        
    }
}
