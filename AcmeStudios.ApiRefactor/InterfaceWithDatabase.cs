using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AcemStudios.ApiRefactor.Data;
using AcemStudios.ApiRefactor.DTOs;

using AcmeStudios.ApiRefactor.Entities;
using AcmeStudios.ApiRefactor.Responses;

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AcemStudios.ApiRefactor
{
    public class InterfaceWithDatabase
    {
        public InterfaceWithDatabase()
        {

        }

        public async Task<ServiceResponse<List<GetStudioItemDto>>> AddStudioItem(AddStudioItemDto newStudioItem)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);

            IConfigurationRoot configuration = builder.Build();

            string conn = configuration.GetConnectionString("StudioConnection");

            var optionsBuilder = new DbContextOptionsBuilder<Cont>();
            optionsBuilder.UseSqlServer(conn);


            using (Cont _cont = new Cont(optionsBuilder.Options))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AutoMapperProfile>();
                });

                var _mapper = config.CreateMapper();

                StudioItem item = _mapper.Map<StudioItem>(newStudioItem);
                await _cont.StudioItems.AddAsync(item);
                await _cont.SaveChangesAsync();

                var serviceResponse = new ServiceResponse<List<GetStudioItemDto>>
                {
                    Data = await _cont.StudioItems.Select(c => _mapper.Map<GetStudioItemDto>(c)).ToListAsync(),
                    Message = $"New item added.  Id: {item.StudioItemId}",
                    Success = true
                };

                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<List<GetStudioItemHeaderDto>>> GetAllStudioHeaderItems()
        {

            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);

            IConfigurationRoot configuration = builder.Build();

            string conn = configuration.GetConnectionString("StudioConnection");

            var optionsBuilder = new DbContextOptionsBuilder<Cont>();
            optionsBuilder.UseSqlServer(conn);

            using (Cont _cont = new Cont(optionsBuilder.Options))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AutoMapperProfile>();
                });

                var _mapper = config.CreateMapper();

                var serviceResponse = new ServiceResponse<List<GetStudioItemHeaderDto>>
                {
                    Data = await _cont.StudioItems.Select(c => _mapper.Map<GetStudioItemHeaderDto>(c)).ToListAsync(),
                    Message = "Here's all the items in your studio",
                    Success = true
                };

                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<GetStudioItemDto>> GetStudioItemById(int id)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);

            IConfigurationRoot configuration = builder.Build();

            string conn = configuration.GetConnectionString("StudioConnection");

            var optionsBuilder = new DbContextOptionsBuilder<Cont>();
            optionsBuilder.UseSqlServer(conn);

            using (Cont _cont = new Cont(optionsBuilder.Options))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AutoMapperProfile>();
                });

                var _mapper = config.CreateMapper();
                var item = await _cont.StudioItems
                .Where(item => item.StudioItemId == id)
                .Include(type => type.StudioItemType)
                .FirstOrDefaultAsync();

                var serviceResponse = new ServiceResponse<GetStudioItemDto>
                {
                    Data = _mapper.Map<GetStudioItemDto>(item),
                    Message = "Here's your selected studio item",
                    Success = true
                };
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItem(UpdateStudioItemDto updatedStudioItem)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);

            IConfigurationRoot configuration = builder.Build();

            string conn = configuration.GetConnectionString("StudioConnection");

            var optionsBuilder = new DbContextOptionsBuilder<Cont>();
            optionsBuilder.UseSqlServer(conn);

            using (Cont _cont = new Cont(optionsBuilder.Options))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AutoMapperProfile>();
                });
                var serviceResponse = new ServiceResponse<GetStudioItemDto>();

                StudioItem studioItem = await _cont.StudioItems
                    .FirstOrDefaultAsync(c => c.StudioItemId == updatedStudioItem.StudioItemId);
                var _mapper = config.CreateMapper();
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
                    serviceResponse.Message = "Update successful";
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

        public async Task<ServiceResponse<List<GetStudioItemDto>>> DeleteStudioItem(int id)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);

            IConfigurationRoot configuration = builder.Build();

            string conn = configuration.GetConnectionString("StudioConnection");

            var optionsBuilder = new DbContextOptionsBuilder<Cont>();
            optionsBuilder.UseSqlServer(conn);

            using (Cont _cont = new Cont(optionsBuilder.Options))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AutoMapperProfile>();
                });

                var _mapper = config.CreateMapper();
                var serviceResponse = new ServiceResponse<List<GetStudioItemDto>>();

                try
                {
                    StudioItem item = await _cont.StudioItems.FirstAsync(c => c.StudioItemId == id);
                    _cont.Remove(item);
                    await _cont.SaveChangesAsync();

                    serviceResponse.Data = await _cont.StudioItems.Select(c => _mapper.Map<GetStudioItemDto>(c)).ToListAsync();
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Item deleted";
                }
                catch (Exception ex)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = ex.Message;
                }

                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<List<StudioItemType>>> GetAllStudioItemTypes()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);

            IConfigurationRoot configuration = builder.Build();

            string conn = configuration.GetConnectionString("StudioConnection");

            var optionsBuilder = new DbContextOptionsBuilder<Cont>();
            optionsBuilder.UseSqlServer(conn);

            using (Cont _cont = new Cont(optionsBuilder.Options))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AutoMapperProfile>();
                });

                var _mapper = config.CreateMapper();
                var serviceResponse = new ServiceResponse<List<StudioItemType>>
                {
                    Data = await _cont.StudioItemTypes.OrderBy(s => s.Value).ToListAsync(),
                    Message = "Item types fetched",
                    Success = true
                };

                return serviceResponse;
            }
        }
    }
}
