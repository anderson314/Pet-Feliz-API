using System;
using System.Collections.Generic;
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
    public class ServicoController : ControllerBase
    {
        
        [HttpPost("Solicitar")]
        public async Task<IActionResult> solicitarServico(Servico novoServico)
        {
            DateTime dataAtual = DateTime.Today;
            DateTime horaAtual = DateTime.Now;
            
            //Atribuir a data e hora acima à requisição o JSON
            novoServico.DataSolicitacao = dataAtual;
            novoServico.HoraSolicitacao = horaAtual;

            //O estado do serviço será marcado automaticamente como solicitado
            novoServico.Estado = EstadoSolicitacao.Solicitado;

            await _context.Servico.AddAsync(novoServico);
            await _context.SaveChangesAsync();

            List<Servico> servicos = await _context.Servico.ToListAsync();

            return Ok(servicos);
        }


        private readonly DataContext _context;
        public ServicoController(DataContext context)
        {
            _context = context;
        }

    }
}