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
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> cadastrarUsuarioAsync(Usuario novoUsuario)
        {
            //Todo usuário cadastrado receberá valor não disponível
            novoUsuario.Disponivel = false;

            //Pegar data atual e colocá-la no JSON
            DateTime data = DateTime.Today;
            novoUsuario.DataCadastro = data;

            await _context.Usuario.AddAsync(novoUsuario);
            await _context.SaveChangesAsync();

            List<Usuario> usuarios = await _context.Usuario.ToListAsync();

            return Ok(usuarios);
        }


        //Método para listar proprietários
        [HttpGet("Proprietarios")]
        public async Task<IActionResult> listarProprietarios()
        {
            List<Usuario> Proprietarios = await _context.Usuario.
                Where(tipoConta => tipoConta.TipoConta == TipoConta.Proprietario).
                ToListAsync();

            return Ok(Proprietarios);
        }

        //Listar Dog Walkers - Página de localizar Dog Walkers
        [HttpGet("DogWalkers")]
        public async Task<IActionResult> listarDogWalkers()
        {
            //Ideias
            //Aplicar um where para buscar o dog walker mais proximo, através da latitude e longitude
            //Busca será feita pelo token

            List<Usuario> dogWalkers = await _context.Usuario
                .Include(valor => valor.ServicoDogWalker)
                .Where(tipoConta => tipoConta.TipoConta == TipoConta.DogWalker)
                .ToListAsync();

            return Ok(dogWalkers);
        }


        //Deletar
        [HttpDelete]
        public async Task<IActionResult> deletarUsuario()
        {
            //Posteriormente, pegar usuario pelo token kk

            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(user => user.Id == 1);

            _context.Remove(usuario);
            await _context.SaveChangesAsync();

            string mensagem = "Usuário removido";

            return Ok(mensagem);
        }

        private readonly DataContext _context;
        public UsuarioController(DataContext context)
        {
            _context = context;
        }
    }
}