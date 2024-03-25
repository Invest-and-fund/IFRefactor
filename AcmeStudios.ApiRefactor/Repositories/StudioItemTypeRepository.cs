using AcmeStudios.ApiRefactor.Data;
using AcmeStudios.ApiRefactor.Entities;

namespace AcmeStudios.ApiRefactor.Repositories;

public class StudioItemTypeRepository : EfCoreRepository<StudioItemType, EFStudioDbContext>
{
    public StudioItemTypeRepository(EFStudioDbContext context) : base(context)
    {

    }
}