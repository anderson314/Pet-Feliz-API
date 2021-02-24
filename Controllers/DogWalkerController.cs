using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetFelizApi.Data;
using PetFelizApi.Models;

namespace PetFelizApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    
    public class DogWalkerController: ControllerBase
    {

        //Método responsável por listar todos os Dog Walkers
        [HttpGet("ListarDogWalker")]
        public IActionResult GetAll()
        {
            List<DogWalker> dogWalkers = _context.DogWalker.ToList();
            return Ok(dogWalkers);
        }
        

        //Método responsável por Cadastrar um Dog Walker
        [HttpPost]
        public IActionResult CadastrarDogWalker(DogWalker novoDogWalker)
        {
            _context.DogWalker.Add(novoDogWalker);
            _context.SaveChanges();

            //Listar todos os Dog Walkers cadastrados
            List<DogWalker> dogWalkers = _context.DogWalker.ToList();

            return Ok(dogWalkers);
        }

        private readonly DataContext _context;
        public DogWalkerController(DataContext context)
        {
            _context = context;
        }
    }
}