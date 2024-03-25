using AcmeStudios.ApiRefactor.Data;
using AcmeStudios.ApiRefactor.Entities;

namespace AcmeStudios.ApiRefactor.Repositories;

public class StudioItemRepository : EfCoreRepository<StudioItem, EFStudioDbContext>
{
    public StudioItemRepository(EFStudioDbContext context) : base(context)
    {

    }
}