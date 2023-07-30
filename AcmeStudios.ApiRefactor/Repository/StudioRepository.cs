using AcmeStudios.ApiRefactor.Data;
using AcmeStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeStudios.ApiRefactor.Repository
{
    public class StudioRepository : IStudioRepository
    { 
        private readonly IMapper _mapper;

        public StudioRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        private DbContextOptions<StudioDbContext> GetDbContextOptions()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
            var configuration = builder.Build();
            var conn = configuration.GetConnectionString("StudioConnection");
            var optionsBuilder = new DbContextOptionsBuilder<StudioDbContext>();
            optionsBuilder.UseSqlServer(conn);
            return optionsBuilder.Options;
        }

        public async Task<ServiceResponse<ICollection<StudioItemType>>> GetAllStudioItemTypes()
        {
            using (StudioDbContext _cont = new StudioDbContext(GetDbContextOptions()))
            {
                var serviceResponse = new ServiceResponse<ICollection<StudioItemType>>
                {
                    Data = await _cont.StudioItemTypes.OrderBy(s => s.Value).ToListAsync(),
                    Message = "Studio item types fetched",
                    Success = true
                };

                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<ICollection<GetStudioItemHeaderDto>>> GetAllStudioHeaderItems()
        {
            using (StudioDbContext _cont = new StudioDbContext(GetDbContextOptions()))
            {
                var serviceResponse = new ServiceResponse<ICollection<GetStudioItemHeaderDto>>
                {
                    Data = await _cont.StudioItems.Select(c => _mapper.Map<GetStudioItemHeaderDto>(c)).ToListAsync(),
                    Message = "Studio header items fetched",
                    Success = true
                };

                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<GetStudioItemDto>> GetStudioItemById(int id)
        {
            using (StudioDbContext _cont = new StudioDbContext(GetDbContextOptions()))
            {
                var item = await _cont.StudioItems
                .Where(item => item.StudioItemId == id)
                .Include(type => type.StudioItemType)
                .FirstOrDefaultAsync();

                var serviceResponse = new ServiceResponse<GetStudioItemDto>
                {
                    Data = _mapper.Map<GetStudioItemDto>(item),
                    Message = $"Studio item fetched. Id:{item.StudioItemId}",
                    Success = true
                };
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<ICollection<GetStudioItemDto>>> AddStudioItem(AddStudioItemDto newStudioItem)
        {
            using (StudioDbContext _cont = new StudioDbContext(GetDbContextOptions()))
            {
                StudioItem item = _mapper.Map<StudioItem>(newStudioItem);
                await _cont.StudioItems.AddAsync(item);
                await _cont.SaveChangesAsync();

                var serviceResponse = new ServiceResponse<ICollection<GetStudioItemDto>>
                {
                    Data = await _cont.StudioItems.Select(c => _mapper.Map<GetStudioItemDto>(c)).ToListAsync(),
                    Message = $"Studio item added. Id: {item.StudioItemId}",
                    Success = true
                };

                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItem(UpdateStudioItemDto updatedStudioItem)
        {
            using (StudioDbContext _cont = new StudioDbContext(GetDbContextOptions()))
            {
                var serviceResponse = new ServiceResponse<GetStudioItemDto>();

                StudioItem studioItem = await _cont.StudioItems
                    .FirstOrDefaultAsync(c => c.StudioItemId == updatedStudioItem.StudioItemId);
                try
                {
                    studioItem.Acquired = updatedStudioItem.Acquired;
                    studioItem.Description = updatedStudioItem.Description;
                    studioItem.Eurorack = updatedStudioItem.Eurorack;
                    studioItem.Name = updatedStudioItem.Name;
                    studioItem.Price = updatedStudioItem.Price;
                    studioItem.SerialNumber = updatedStudioItem.SerialNumber;
                    studioItem.Sold = updatedStudioItem.Sold;
                    studioItem.SoldFor = updatedStudioItem.SoldFor;
                    studioItem.StudioItemType = updatedStudioItem.StudioItemType;

                    serviceResponse.Data = _mapper.Map<GetStudioItemDto>(studioItem);
                    serviceResponse.Message = $"Studio item updated. Id:{studioItem.StudioItemId}";
                    serviceResponse.Success = true;
                }
                catch (Exception ex)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = ex.Message;
                }

                _cont.StudioItems.Update(studioItem);
                await _cont.SaveChangesAsync();

                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<ICollection<GetStudioItemDto>>> DeleteStudioItem(int id)
        {
            using (StudioDbContext _cont = new StudioDbContext(GetDbContextOptions()))
            {
                var serviceResponse = new ServiceResponse<ICollection<GetStudioItemDto>>();

                try
                {
                    StudioItem item = await _cont.StudioItems.FirstAsync(c => c.StudioItemId == id);
                    _cont.Remove(item);
                    await _cont.SaveChangesAsync();

                    serviceResponse.Data = await _cont.StudioItems.Select(c => _mapper.Map<GetStudioItemDto>(c)).ToListAsync();
                    serviceResponse.Success = true;
                    serviceResponse.Message = $"Studio item deleted. Id:{item.StudioItemId}";
                }
                catch (Exception ex)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = ex.Message;
                }

                return serviceResponse;
            }
        }
    }
}
