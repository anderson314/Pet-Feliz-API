using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
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

        
        [HttpPost]
        //Deverá somente mandar os ids dos cães nas solicitações
        public async Task<IActionResult> AdicionarCaoServico(CaoServico novoCaoServico)
        {
            
            Usuario Proprietario = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == PegarIdUsuarioToken());

            //Buscar o cão passado no JSON
            int idCao = novoCaoServico.CaoId;
            Cao cao = await _context.Cao.Include(prop => prop.Proprietario)
                .FirstOrDefaultAsync(cao => cao.Id == idCao);

            //Pegar o id do proprietário responsável pelo cão
            int idProprietario = cao.Proprietario.Id;
            
            if(Proprietario.Id != idProprietario)
            {
                return BadRequest("O cão não pertence a " + Proprietario.Nome);
            }

            //Buscar o último servico do proprietario
            Servico servico = await _context.Servico.OrderBy(prop => prop.ProprietarioId == idProprietario)
                //.Include(usua => usua.Usuarios).ThenInclude(details => details.Usuario)
                //.Include(caes => caes.Caes).ThenInclude(details => details.Cao)
                .LastAsync();

            //O servico a qual o cão está sendo associado será o serviço buscado acima
            novoCaoServico.Servico = servico;
            novoCaoServico.Cao = cao;

            await _context.CaesServico.AddAsync(novoCaoServico);
            await _context.SaveChangesAsync();

            return Ok("Cão adicionado ao servico com sucesso.");
        }


        //Retornará o Id do usuário logado
        private int PegarIdUsuarioToken()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
        

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CaoServicoController(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}