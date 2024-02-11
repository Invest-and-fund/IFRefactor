using System;
using System.ComponentModel.DataAnnotations;

namespace AcmeStudios.ApiRefactor.DTOs
{
    public record StudioItemForUpdateDto(
        [Required] int StudioItemId,
        DateTime Acquired,
        DateTime? Sold,
        [Required] string Name,
        [Required] string Description,
        [Required] string SerialNumber,
        decimal Price,
        decimal? SoldFor,
        bool Eurorack,
        int StudioItemTypeId
        );
}
