using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFelizApi.Data;
using PetFelizApi.Models;
using PetFelizApi.Models.Enuns;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace PetFelizApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [AllowAnonymous]
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
            //Adiciona 2 horas, pois o servidor está 2 horas adiantadas
            DateTime data = DateTime.Today.AddHours(2);
            novoUsuario.DataCadastro = data;

            await _context.Usuario.AddAsync(novoUsuario);
            await _context.SaveChangesAsync();

            List<Usuario> usuarios = await _context.Usuario.ToListAsync();

            return Ok(usuarios);
        }

        [AllowAnonymous]
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
                return Ok(CriarToken(usuario));
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

            //Pega o id do usuário logado -- token
            int id = PegarIdUsuarioToken();

            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == id);

            if(usuario.TipoConta == TipoConta.DogWalker)
            {
                return BadRequest("O Dog Walker não pode ver outros Dog Walkers.");
            }
            
            List<Usuario> dogWalkers = await _context.Usuario
                .Include(valor => valor.ServicoDogWalker)
                .Where(filtro => filtro.TipoConta == TipoConta.DogWalker && filtro.ServicoDogWalker != null)
                .ToListAsync();
            
            

            return Ok(dogWalkers);
        }

        [HttpPut("AtualizarLocalizacao")]
        public async Task<IActionResult> alterarLocalizacao(Usuario usuarioLocaliz)
        {
            //Pega o id do usuário logado -- token
            int id = PegarIdUsuarioToken();

            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == id);

            usuario.Latitude = usuarioLocaliz.Latitude;
            usuario.Longitude = usuarioLocaliz.Longitude;

            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();

            return Ok(usuario);
        }

        //Deve ser passado o token no header
        [HttpGet]
        public async Task<IActionResult> informacoesUsuario()
        {
            Usuario usuario = await _context.Usuario.Include(sd => sd.ServicoDogWalker)
            .FirstOrDefaultAsync(id => id.Id == PegarIdUsuarioToken());

            return Ok(usuario);
        }


        [AllowAnonymous]
        //Deletar
        [HttpDelete]
        public async Task<IActionResult> deletarUsuario()
        {
            //Posteriormente, pegar usuario pelo token kk

            Usuario usuario = await _context.Usuario.FirstOrDefaultAsync(user => user.Id == 7);

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

        //Retorna o Id do usuário logado
        private int PegarIdUsuarioToken()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
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
    
        //Função que criará o token
        private string CriarToken(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(14),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public UsuarioController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
    }
}