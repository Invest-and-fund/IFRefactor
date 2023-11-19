using System.ComponentModel.DataAnnotations;

namespace AcmeStudios.ApiRefactor.Models.DTOs
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
