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