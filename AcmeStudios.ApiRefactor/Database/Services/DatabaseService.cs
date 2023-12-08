using AcemStudios.ApiRefactor.Database.Context;
using AcemStudios.ApiRefactor.DTOs;
using AcemStudios.ApiRefactor.Database.Models;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;

namespace AcemStudios.ApiRefactor
{
    public class DatabaseService
    {
        public DatabaseService()
        {

        }

        private static readonly string connectionString = 
            Startup.ApiConfigurationSettings.ConnectionStrings["StudioConnection"];
    
        DbContextOptionsBuilder<Context> optionsBuilder = new DbContextOptionsBuilder<Context>();

        public async Task<ServiceResponse<List<GetStudioItemDto>>> AddStudioItem(AddStudioItemDto newStudioItem)
        {
            optionsBuilder.UseSqlServer(connectionString);

            using (Context _cont = new Context(optionsBuilder.Options))
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

            optionsBuilder.UseSqlServer(connectionString);

            using (Context _cont = new Context(optionsBuilder.Options))
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
            optionsBuilder.UseSqlServer(connectionString);

            using (Context _cont = new Context(optionsBuilder.Options))
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
            optionsBuilder.UseSqlServer(connectionString);

            using (Context _cont = new Context(optionsBuilder.Options))
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
            optionsBuilder.UseSqlServer(connectionString);

            using (Context _cont = new Context(optionsBuilder.Options))
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
            optionsBuilder.UseSqlServer(connectionString);
            
            using (Context _cont = new Context(optionsBuilder.Options))
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

    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public bool Success { get; set; } = false;

        public string Message { get; set; } = null;
    }

}
