using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetFelizApi.Models;
using PetFelizApi.Models.Enuns;

namespace PetFelizApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderDogWalkerController : ControllerBase
    {
        private static List<EnderecoDogWalker> endDogW = new List<EnderecoDogWalker>
        {
            new EnderecoDogWalker (),
            new EnderecoDogWalker { IdEndDogW = 1, Estado = EstadoBrasileiro.SP, Cidade = "São Paulo", Bairro = "Vila Maria", Rua = "João Neves", NmrEndereco = "145" },
            new EnderecoDogWalker { IdEndDogW = 2, Estado = EstadoBrasileiro.SP, Cidade = "São Paulo", Bairro = "Vila Guilherme", Rua = "Maria França", NmrEndereco = "236" }
        };


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(endDogW);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(endDogW.FirstOrDefault(pegarPorId => pegarPorId.IdEndDogW == id));
        }
    }
}