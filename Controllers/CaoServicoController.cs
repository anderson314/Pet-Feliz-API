using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFelizApi.Data;
using PetFelizApi.Models;

namespace PetFelizApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaoServicoController : ControllerBase
    {

        //CONTINUAR APÓS FAZER A CONTROLLER DE UsuariosServico
        /*
        [HttpPost]
        public async Task<IActionResult> AdicionarCaoServico(CaoServico novoCaoServico)
        {
            int prorietario = 3;
            
            Servico servico = await _context.Servico.LastAsync()
                .Where();

            await _context.CaesServico.AddAsync(novoCaoServico);
            await _context.SaveChangesAsync();

             
            
        }
        */

        private readonly DataContext _context;
        public CaoServicoController(DataContext context)
        {
            _context = context;
        }
    }
}