using System.ComponentModel.DataAnnotations;

namespace AcmeStudios.ApiRefactor.Application.DTOs
{
    public sealed class UpdateStudioItemDto
    {
        public UpdateStudioItemDto(int studioItemId, 
            DateTime acquired, 
            DateTime? sold, 
            string name, 
            string description, 
            string serialNumber, 
            bool eurorack, 
            StudioItemTypeDto studioItemType,
            decimal price = 10.00M,
            decimal soldFor = 0M)
        {
            StudioItemId = studioItemId;
            Acquired = acquired;
            Sold = sold;
            Name = name;
            Description = description;
            SerialNumber = serialNumber;
            Price = price;
            SoldFor = soldFor;
            Eurorack = eurorack;
            StudioItemType = studioItemType;
        }

        public int StudioItemId { get; init; }
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
        public StudioItemTypeDto StudioItemType { get; init; }
    }
}
