using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFelizApi.Data;
using PetFelizApi.Models;

namespace PetFelizApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaoController : ControllerBase
    {
        
        //Método responsável por adicionar um cão
        [HttpPost]
        public async Task<IActionResult> cadastrarCao(Cao novoCao)
        {
            int id = 1;

            novoCao.Proprietario = await _context.Proprietario.FirstOrDefaultAsync(pr => pr.Id == id);

            await _context.Cao.AddAsync(novoCao);
            await _context.SaveChangesAsync();

            List<Cao> caes = await _context.Cao.ToListAsync();

            return Ok(caes);
        }
        

        private readonly DataContext _context;
        public CaoController(DataContext context)
        {
            _context = context;
        }
    }
}