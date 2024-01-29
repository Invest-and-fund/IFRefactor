using AcemStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.Models;
using AcmeStudios.ApiRefactor.Utilities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AcmeStudios.ApiRefactor.Controllers
{
    [Route("charges")]
    public class PaymentChargeController : ControllerBase
    {
        public readonly DbContext _context;
        public PaymentChargeController(DbContext context)
        {
            _context = context;
        }

        [HttpPost("")]
        //[DisableCors]
        public async Task<ActionResult<GenericResponse>> CreateChargeAsync([FromForm] PaymentChargeDto requestForm)
        {
            GenericResponse res = new GenericResponse();
            if (!ModelState.IsValid)
            {
                res.Success = false;
                res.Message = "Please provide the required fields. Unable to process that request";
                res.Errors = ModelState.Values.SelectMany(v => v.Errors)
                           .Select(y => y.ErrorMessage)
                           .ToList();
                return BadRequest(res);
            }
            var p = new PaymentCharge();
            p.CopyPropertiesFrom(requestForm);
            // Add the charge to the context
            _context.Add(p);
            // Save the context changes in the database
            await _context.SaveChangesAsync();
            res.Success = true;
            res.Message = "Charge created successfully";
            return Ok(res);
        }
    }
}
