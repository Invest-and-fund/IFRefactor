using AcmeStudios.ApiRefactor.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AcmeStudios.ApiRefactor
{
    public static class ServiceResponseExtensions
    {
        public static IActionResult GetApiResponse<T>(this ServiceResponse<T> serviceResponse)
        {
            return serviceResponse.Success 
                ? new OkObjectResult(serviceResponse) 
                : new BadRequestObjectResult(serviceResponse);
        }
    }
}
