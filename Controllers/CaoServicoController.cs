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
    public class CaoServicoController : ControllerBase
    {

        
        [HttpPost]
        //Deverá somente mandar os ids dos cães nas solicitações
        public async Task<IActionResult> AdicionarCaoServico(CaoServico novoCaoServico)
        {
            
            //Declarara uma variável para armazenar o id do cão
            int idCao;

            //Buscar o cão passado no JSON
            idCao = novoCaoServico.CaoId;
            Cao cao = await _context.Cao.Include(prop => prop.Proprietario)
                .FirstOrDefaultAsync(cao => cao.Id == idCao);

            //Pegar o id do proprietário responsável pelo cão
            int idProprietario = cao.Proprietario.Id;
            
            //Buscar o último servico do proprietario
            Servico servico = await _context.Servico.OrderBy(prop => prop.ProprietarioId == idProprietario)
                //.Include(usua => usua.Usuarios).ThenInclude(details => details.Usuario)
                //.Include(caes => caes.Caes).ThenInclude(details => details.Cao)
                .LastAsync();

            //O servico a qual o cão está sendo associado será o serviço buscado acima
            novoCaoServico.Servico = servico;

            await _context.CaesServico.AddAsync(novoCaoServico);
            await _context.SaveChangesAsync();

            return Ok("Cão adicionado ao servico com sucesso.");
        }
        

        private readonly DataContext _context;
        public CaoServicoController(DataContext context)
        {
            _context = context;
        }
    }
}