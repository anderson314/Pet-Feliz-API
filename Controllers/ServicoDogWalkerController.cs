using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetFelizApi.Data;
using PetFelizApi.Models;
using PetFelizApi.Controllers;
using System.Collections.Generic;
using System.Linq;
using PetFelizApi.Models.Enuns;
using Microsoft.EntityFrameworkCore;

namespace PetFelizApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicoDogWalkerController : ControllerBase
    {

        //Método responsável por adicionar as informações de servico do Dog Walker
        [HttpPost]
        public async Task<IActionResult> adcInformacaoServicoDogW(InformacoesServicoDogWalker novoInformacaoServDogW)
        {
            int IdDogW;

            //Pega o Id do usuário
            IdDogW = novoInformacaoServDogW.DogWalkerId;
            //Busca o usuário do Id acima
            Usuario dogW = await _context.Usuario.FirstOrDefaultAsync(dogW => dogW.Id == IdDogW);
            //Verifica se o usuário é um proprietário. Se for, resultará em uma BadRequest
            if(dogW.TipoConta == TipoConta.Proprietario)
            {
                return BadRequest("As informações de servico não podem ser inseridas para um Proprietario");
            }

            await _context.ServicoDogWalker.AddAsync(novoInformacaoServDogW);
            await _context.SaveChangesAsync();
            
            //Listar todos os Dog Walkers
            List<Usuario> dogWalkers = await _context.Usuario.
                Where(tipoConta => tipoConta.TipoConta == TipoConta.DogWalker).
                ToListAsync();

            return Ok(dogWalkers);
        }

        


        private readonly DataContext _context;
        public ServicoDogWalkerController(DataContext context)
        {
            _context = context;
        }
    }
}