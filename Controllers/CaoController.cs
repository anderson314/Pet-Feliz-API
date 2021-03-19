using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFelizApi.Data;
using PetFelizApi.Models;
using PetFelizApi.Models.Enuns;

namespace PetFelizApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaoController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> cadastrarCaoAsync(Cao novoCao)
        {
            //Posteriormente, pegar o ID pelo token

            int idProp = 3;

            novoCao.Proprietario = await _context.Usuario.FirstOrDefaultAsync(prop => prop.Id == idProp);

            await _context.Cao.AddAsync(novoCao);
            await _context.SaveChangesAsync();

            List<Cao> caes = await _context.Cao.ToListAsync();

            return Ok(caes);
        }

        [HttpGet]
        public async Task<IActionResult> listarCaesProprietario()
        {
            Usuario Proprietario = await _context.Usuario.FirstOrDefaultAsync(prop => prop.Id == 3);

            List<Cao> Caes = await _context.Cao.Where(propri => propri.Proprietario == Proprietario)
                .Include(peso => peso.Peso)
                .ToListAsync();

            return Ok(Caes);
        }  


        [HttpDelete]
        public async Task<IActionResult> deletarCao()
        {
            Usuario proprietario = await _context.Usuario.FirstOrDefaultAsync(user => user.Id == 1);

            Cao cao = await _context.Cao.Where(prop => prop.Proprietario == proprietario).FirstOrDefaultAsync();

            _context.Remove(cao);
            await _context.SaveChangesAsync();

            string mensagem = "Cão removido";

            return Ok(mensagem);
        }

        private readonly DataContext _context;
        public CaoController(DataContext context)
        {
            _context = context;
        }

    }
}