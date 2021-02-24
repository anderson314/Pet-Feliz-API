using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFelizApi.Data;
using PetFelizApi.Models;

namespace PetFelizApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class SolicitacaoServicoController : ControllerBase
    {

        //Método para realizar solicitação de servico
        [HttpPost]
        public async Task<IActionResult> SolicitarServicoAsync(SolicitacaoServico solicitarServico)
        {
            


            DateTime dataAtual =  DateTime.Today;
            DateTime horaAtual = DateTime.Now;

            solicitarServico.DataSolicitacao = dataAtual;
            solicitarServico.HoraSolicitacao = horaAtual;
            
            await _context.SolicitacaoServico.AddAsync(solicitarServico);
            await _context.SaveChangesAsync();

            List<SolicitacaoServico> solicitacoes = await _context.SolicitacaoServico.ToListAsync();

            return Ok(solicitacoes);
        }



        private readonly DataContext _context;
        public SolicitacaoServicoController(DataContext context)
        {
            _context = context;
        }
    }
}