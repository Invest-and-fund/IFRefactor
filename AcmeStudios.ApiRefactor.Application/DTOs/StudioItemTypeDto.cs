namespace AcmeStudios.ApiRefactor.Application.DTOs
{
    public sealed class StudioItemTypeDto
    {
        public StudioItemTypeDto(int studioItemTypeId, string value) 
        {
            StudioItemTypeId = studioItemTypeId;
            Value = value;
        }

        public int StudioItemTypeId { get; init; }
        public string Value { get; init; }
    }
}
