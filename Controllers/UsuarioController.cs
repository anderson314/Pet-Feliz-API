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
        [HttpPost("Cadastrar")]
        public async Task<IActionResult> cadastrarUsuarioAsync(Usuario novoUsuario)
        {
            //Verifica se o usuário já existe.
            if(await UsuarioExiste(novoUsuario.Email))
                return BadRequest("Email já cadastrado");

            CriarPasswordHash(novoUsuario.PasswordString, out byte[] passwordHash, out byte[] passwordSalt);

            novoUsuario.PasswordString = string.Empty;
            novoUsuario.PasswordHash = passwordHash;
            novoUsuario.PasswordSalt = passwordSalt;


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

        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(Usuario credenciaisUsuario)
        {
            //Busca o usuário através do email
            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(email => 
                email.Email.ToLower().Equals(credenciaisUsuario.Email.ToLower()));

            if(usuario == null)
            {
                return BadRequest("O email digitado não existe.");
            }
            else if(!VerificarPasswordHash(credenciaisUsuario.PasswordString, 
                                            usuario.PasswordHash, usuario.PasswordSalt))
            {
                return BadRequest("Senha incorreta.");
            }
            else
            {
                return Ok(usuario.Id);
            }

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


        
        //Algoritmo de criação de Hash e Salt
        private void CriarPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        //Método que verificará se usuário já existe
        public async Task<bool> UsuarioExiste(string email)
        {
            if(await _context.Usuario.AnyAsync(e => e.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }

        //Verificará se a senha digitada pelo usuário, no login, estiver certa
        private bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {   
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    

        private readonly DataContext _context;
        public UsuarioController(DataContext context)
        {
            _context = context;
        }
    }
}