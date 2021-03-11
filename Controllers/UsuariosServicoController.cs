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
    public class UsuariosServicoController : ControllerBase
    {
        
        [HttpPost]
        public async Task<IActionResult> associarUsuariosServico(UsuariosServico novoUsuariosServico)
        {
            //Posteriormente pegar pelo token

            int UsuarioId = novoUsuariosServico.UsuarioId;
           
            //int idProp = 3;

            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(usu => usu.Id == UsuarioId);

            //Pegar o último serviço solicitado pelo Proprietário, para associar o proprietário a este serviço
            Servico servico = await _context.Servico.OrderBy(prop => prop.ProprietarioId == UsuarioId)
                .Include(usua => usua.Usuarios)
                .LastAsync();
                

            /*Se o usuário adicionado for um Proprietário, então
              será retornado o servico a qual ele foi atribuido.
              Logo em seguida o front-end pegará o id desse servico e, no momento
              de associar o Dog Walker a um serviço, utilizará esse Id
            */
            if(usuario.TipoConta == TipoConta.Proprietario)
            {
                novoUsuariosServico.Servico = servico;
                

                await _context.UsuariosServico.AddAsync(novoUsuariosServico);
                await _context.SaveChangesAsync();

                return Ok(servico);
            }
            else
            {
                await _context.UsuariosServico.AddAsync(novoUsuariosServico);
                await _context.SaveChangesAsync();

                return Ok("Dog Walker atribuido com sucesso.");
            } 
            
        }
        


        private readonly DataContext _context;
        public UsuariosServicoController(DataContext context)
        {
            _context = context;
        }
    }
}