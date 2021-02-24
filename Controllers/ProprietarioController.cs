using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFelizApi.Data;
using PetFelizApi.Models;

namespace PetFelizApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProprietarioController : ControllerBase
    {
        //Método provisório para adicionar Proprietário
        [HttpPost]
        public async Task<IActionResult> adcProprietario(Proprietario novoProprietario)
        {
            await _context.Proprietario.AddAsync(novoProprietario);
            await  _context.SaveChangesAsync();

            List<Proprietario> proprietarios = await _context.Proprietario.ToListAsync();
            
            return Ok(proprietarios);
        } 

        private readonly DataContext _context;
        public ProprietarioController(DataContext context)
        {
            _context = context;
        }
    }
}