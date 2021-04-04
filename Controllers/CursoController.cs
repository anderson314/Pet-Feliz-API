using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFelizApi.Data;
using PetFelizApi.Models;

namespace PetFelizApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CursoController : ControllerBase
    {
        
        [HttpPost("AdicionarCurso")]
        public async Task<IActionResult> adicionarCurso(Curso novoCurso)
        {
            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == PegarIdUsuarioToken());

            novoCurso.InfoServDogW = usuario.ServicoDogWalker;

            await _context.Curso.AddAsync(novoCurso);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //Retornará o Id do usuário logado
        private int PegarIdUsuarioToken()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CursoController(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

    }
}