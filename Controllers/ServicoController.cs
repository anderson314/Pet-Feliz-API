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
        
        //OBS: O Id do proprietário terá de ser mandado na requisição
        //O proprietário deverá ser adicionado primeiro
        [HttpPost("Solicitar")]
        public async Task<IActionResult> solicitarServico(Servico novoServico)
        {
            //Selecionar o Id do usuário no Banco. Posteriormente, pegar pelo token de sessão
            // Buscar o Id do proprietário que está fazendo a solicitação
            int IdProp = 3;

            //Guarda o Id do Proprietário que está fazendo a solicitação
            novoServico.ProprietarioId = IdProp;

            DateTime dataAtual = DateTime.Today;
            DateTime horaAtual = DateTime.Now;
            
            //Atribuir a data e hora acima à requisição o JSON
            novoServico.DataSolicitacao = dataAtual.ToString("dd/MM/yyyy");
            novoServico.HoraSolicitacao = horaAtual.ToString("HH:mm");

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