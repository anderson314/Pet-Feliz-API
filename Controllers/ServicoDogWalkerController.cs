using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetFelizApi.Data;
using PetFelizApi.Models;
using PetFelizApi.Controllers;
using System.Collections.Generic;
using System.Linq;
using PetFelizApi.Models.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

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

            //Busca o usuário
            Usuario dogW = await _context.Usuario.FirstOrDefaultAsync(dogW => dogW.Id == PegarIdUsuarioToken());
            //Verifica se o usuário é um proprietário. Se for, resultará em uma BadRequest
            if(dogW.TipoConta == TipoConta.Proprietario)
            {
                return BadRequest("As informações de servico não podem ser inseridas para um Proprietario");
            }

            //Associa o Dog Walker às informações
            novoInformacaoServDogW.DogWalker = dogW;
            novoInformacaoServDogW.DogWalkerId = dogW.Id;

            await _context.ServicoDogWalker.AddAsync(novoInformacaoServDogW);
            await _context.SaveChangesAsync();
            

            return Ok(dogW);
        }

        
        //Retorna o Id do usuário logado
        private int PegarIdUsuarioToken()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
        

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        public ServicoDogWalkerController(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}