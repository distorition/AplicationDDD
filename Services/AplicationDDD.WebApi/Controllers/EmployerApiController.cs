using AplicatioDDD.Domain.Entities;
using AplicationDDD.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AplicationDDD.WebApi.Controllers
{
    [ApiController]
    [Route("api/EmployerApi")]
    
    public class EmployerApiController : ControllerBase
    {
        private readonly IRepositoryAsync<Employe> _Employes;
        private readonly ILogger<EmployerApiController> Logger;

        public EmployerApiController(IRepositoryAsync<Employe> Employes,ILogger<EmployerApiController> logger)
        {
            _Employes = Employes;
            Logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employes= await _Employes.GetAllAsync().ConfigureAwait(true);
            return Ok(employes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employe = await _Employes.GetTByIdAsync(id);
            if(employe is null)
            {
                return NotFound();
            }
            return Ok(employe);
        }
        
        [HttpGet("Count")]
        public async Task<IActionResult> Count()
        {
            var result=await _Employes.CountAsync().ConfigureAwait(false);
            return Ok(result);
        }
    }
}
