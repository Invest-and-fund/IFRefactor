using AcmeStudios.ApiRefactor.Domain;

namespace AcmeStudios.ApiRefactor.DataAccess.Repositories
{
    public interface IStudioItemTypeRepository
    {
        Task<IEnumerable<StudioItemType>> GetAllAsync();
    }
}
