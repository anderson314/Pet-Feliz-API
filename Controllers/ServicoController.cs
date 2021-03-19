using System;
using System.Collections.Generic;
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

        //Proprietário e Dog Walker
        //Listar os Servicos Gerais
        [HttpGet("ListarServicosGerais")]
        public async Task<IActionResult> listarServicosGerais()
        {
            //Posteriormente, modificar as buscas de acordo com o o tipo de usuário que está solicitando  

            List<Servico> servicosGerais = await _context.Servico.Where(estado => estado.Estado != EstadoSolicitacao.Finalizado)
                .Include(usu => usu.Usuarios).ThenInclude(usua => usua.Usuario)
                .Include(cao => cao.Caes)
                .OrderByDescending(dt => dt.Id)
                .ToListAsync();

            return Ok(servicosGerais);
        }

        //Proprietário e Dog Walker
        //Listar os Servicos Finalizados
        [HttpGet("ListarServicosFinalizados")]
        public async Task<IActionResult> listarServicosFinalizados()
        {
            List<Servico> servicosFinalizados = await _context.Servico.Where(estado => estado.Estado == EstadoSolicitacao.Finalizado)
                .Include(usu => usu.Usuarios).ThenInclude(usua => usua.Usuario)
                .OrderByDescending(dt => dt.Id)
                .ToListAsync();

            return Ok(servicosFinalizados);
        }

        
        //Nome : Cancelar Servico
        //Atores: proprietário
        //OBS: O id DEVE vir pela url
        [HttpPut("Cancelar/{id}")]
        public async Task<IActionResult> cancelarServico(int id)
        {
            //Pegar pelo token
            //Depois verificar o tipo de usuário pelo token

            int idServico = id;
            Servico servico = await _context.Servico.FirstOrDefaultAsync(id => id.Id == idServico);

            //Verificar se, o serviço que está a ser cencelado, está em estado de aceito ou solicitado
            if(servico.Estado == EstadoSolicitacao.Aceito || servico.Estado == EstadoSolicitacao.Solicitado)
            {
                 servico.Estado = EstadoSolicitacao.Cancelado;
            }
            else
            {
                return BadRequest("O serviço já foi cancelado.");
            }

            //Define o estado do servico para cancelado
            servico.Estado = EstadoSolicitacao.Cancelado;

            _context.Servico.Update(servico);
            await _context.SaveChangesAsync();

            return Ok(servico);
        }
        
        

        // Nome : Iniciar Servico
        // Atores : Proprietário
        // OBS : O id DEVE vir pela url
        [HttpPut("Iniciar/{id}")]
        public async Task<IActionResult> iniciarServico(int id)
        {
            //Pegar pelo token
            //Depois verificar o tipo de usuário pelo token

            int idServico = id;
            Servico servico = await _context.Servico.FirstOrDefaultAsync(id => id.Id == idServico);

            //Verifica se o estado do serviço não está como aceito
            if(servico.Estado != EstadoSolicitacao.Aceito)
            {
                 return BadRequest("Impossível iniciar o serviço. O serviço deve estar em estado de Aceito.");
            }

            //Atribuir hora do inicio do servico
            DateTime horaAtual = DateTime.Now;
            servico.HoraInicio = horaAtual.ToString("HH:mm");

            servico.Estado = EstadoSolicitacao.EmAndamento;

            _context.Servico.Update(servico);
            await _context.SaveChangesAsync();

            return Ok(servico);
        }


        // Nome : Finalizar Servico
        // Atores : Proprietário
        // OBS : O id DEVE vir pela url
        [HttpPut("FinalizarServico/{id}")]
        public async Task<IActionResult> finalizarServico(int id)
        {
            int idServico = id;
            Servico servico = await _context.Servico.FirstOrDefaultAsync(id => id.Id == idServico);

            if(servico.Estado != EstadoSolicitacao.EmAndamento)
            {
                return BadRequest("Não é possível finalizar o serviço, ele deve ter sido iniciado, antes");
            }

            //Atribuir hora do inicio do servico
            DateTime horaAtual = DateTime.Now;
            servico.HoraTermino = horaAtual.ToString("HH:mm");

            servico.Estado = EstadoSolicitacao.Finalizado;

            _context.Servico.Update(servico);
            await _context.SaveChangesAsync();

            return Ok(servico);
        }



        private readonly DataContext _context;
        public ServicoController(DataContext context)
        {
            _context = context;
        }

    }
}