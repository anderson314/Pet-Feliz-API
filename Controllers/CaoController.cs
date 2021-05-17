using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            //Busca o usuário de acordo com o token
            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(prop => prop.Id == PegarIdUsuarioToken());

            if(usuario.TipoConta == TipoConta.DogWalker)
            {
                return BadRequest("Um Dog Walker não pode ter cães.");
            }

            //Busca o peso de acordo com o Id
            int idPesoCao = novoCao.PesoId;
            PesoCao pesoCao = await _context.PesoCao.FirstOrDefaultAsync(pesoId => pesoId.Id == idPesoCao);

            novoCao.Proprietario = usuario;
            novoCao.Peso = pesoCao;

            await _context.Cao.AddAsync(novoCao);
            await _context.SaveChangesAsync();

            List<Cao> caes = await _context.Cao.Where(c => c.Proprietario == usuario).ToListAsync();

            return Ok(caes);
        }

        [HttpGet("{idProp}")]
        public async Task<IActionResult> listarCaesProprietario(int idProp)
        {
            Usuario Proprietario = await _context.Usuario.FirstOrDefaultAsync(prop => prop.Id == PegarIdUsuarioToken());

            List<Cao> Caes = await _context.Cao.Where(propri => propri.Proprietario.Id == idProp)
                .Include(peso => peso.Peso)
                .ToListAsync();

            return Ok(Caes);
        }  


        [HttpDelete("DeletarCao/{idCao}")]
        public async Task<IActionResult> deletarCao(int idCao)
        {
            Usuario proprietario = await _context.Usuario.FirstOrDefaultAsync(user => user.Id == PegarIdUsuarioToken());

            Cao cao = await _context.Cao.FirstOrDefaultAsync(it => it.Id == idCao);

            if(cao.Proprietario != proprietario)
            {
                BadRequest("O usuário logado não pode deletar este cão.");
            }

            _context.Remove(cao);
            await _context.SaveChangesAsync();

            return Ok("Cão removido.");
        }

        //Retorna o Id do usuário logado
        private int PegarIdUsuarioToken()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CaoController(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

    }
}