using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFelizApi.Data;
using PetFelizApi.Models;
using PetFelizApi.Models.Enuns;

namespace PetFelizApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosServicoController : ControllerBase
    {
        
        [HttpPost]
        public async Task<IActionResult> associarUsuariosServico(UsuariosServico novoUsuariosServico)
        {

            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(usu => usu.Id == PegarIdUsuarioToken());

            //Pegar o último serviço solicitado pelo Proprietário, para associar o proprietário a este serviço
            Servico servico = await _context.Servico.OrderBy(prop => prop.ProprietarioId == PegarIdUsuarioToken())
                .Include(usua => usua.Usuarios)
                .LastAsync();
                
            //Busca o serviço em que o proprietário está associado
            UsuariosServico usuSer = await _context.UsuariosServico
                .FirstOrDefaultAsync(usu => usu.UsuarioId == PegarIdUsuarioToken() && usu.Servico == servico);

            /*Se o usuário adicionado for um Proprietário, então
              será retornado o servico a qual ele foi atribuido.
              Logo em seguida o front-end pegará o id desse servico e, no momento
              de associar o Dog Walker a um serviço, utilizará esse Id
            */
            if(usuario.TipoConta == TipoConta.Proprietario)
            {
                
                //Se não achar nada, vai associar o proprietário ao serviço
                if(usuSer == null)
                {
                    novoUsuariosServico.Servico = servico;
                    novoUsuariosServico.Usuario = usuario;

                    await _context.UsuariosServico.AddAsync(novoUsuariosServico);
                    await _context.SaveChangesAsync();

                    return Ok(servico);
                }
                //Caso achar, vai pedir para associar o Dog Walker
                else
                {
                    //Pega o id do JSON e o busca
                    int idDogWalker = novoUsuariosServico.UsuarioId;
                    Usuario dogWalker = await _context.Usuario.FirstOrDefaultAsync(usu => usu.Id == idDogWalker);

                    //Associa o dog walker ao servio
                    novoUsuariosServico.Servico = servico;
                    novoUsuariosServico.Usuario = dogWalker;

                    await _context.UsuariosServico.AddAsync(novoUsuariosServico);
                    await _context.SaveChangesAsync();

                    return Ok(servico);
                }

                
            }
            else
                return BadRequest("Este usuário não tem permissão pra realizar esta ação.");
            
        }
        
        private int PegarIdUsuarioToken()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }


        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuariosServicoController(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}