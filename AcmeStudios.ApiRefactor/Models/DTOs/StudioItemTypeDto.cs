namespace AcmeStudios.ApiRefactor.Models.DTOs
{
    using System.ComponentModel.DataAnnotations;

    namespace AcmeStudios.ApiRefactor.Models.Dto
    {
        public class StudioItemTypeDto
        {
            public int StudioItemTypeId { get; set; }

            [Required]
            public string Value { get; set; }
        }
    }

}
