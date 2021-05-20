using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFelizApi.Data;
using PetFelizApi.Models;

namespace Pet_Feliz_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioAvaliacaoController : ControllerBase
    {
        public async Task<ActionResult> cadastrarAvaliacao(Avaliacao avaliacao)
        {

            return null;
        }

        //Retorna o Id do usuário logado
        private int PegarIdUsuarioToken()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuarioAvaliacaoController(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

    }
}