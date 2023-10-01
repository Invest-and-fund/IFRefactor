using System.ComponentModel.DataAnnotations;

namespace AcmeStudios.ApiRefactor.Application.DTOs
{
    public sealed class AddStudioItemDto
    {
        public AddStudioItemDto(DateTime acquired,
            string name,
            string description,
            string serialNumber,
            int studioItemTypeId,
            DateTime? sold,
            decimal price = 10.00M,
            decimal soldFor = 0M,
            bool eurorack = false)
        {
            Acquired = acquired;
            Sold = sold;
            Name = name;
            Description = description;
            SerialNumber = serialNumber;
            Price = price;
            SoldFor = soldFor;
            Eurorack = eurorack;
            StudioItemTypeId = studioItemTypeId;
        } 

        public DateTime Acquired { get; init; }
        public DateTime? Sold { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string Description { get; init; }
        [Required]
        public string SerialNumber { get; init; }
        public decimal Price { get; init; }
        public decimal SoldFor { get; init; }
        public bool Eurorack { get; init; }
        public int StudioItemTypeId { get; init; }
    }
}
