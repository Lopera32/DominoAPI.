using Domino.Core.DTOs;
using Domino.Core.Entities;
using Domino.Core.Interfaces;
using Domino.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Domino.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DominoFullGameController : ControllerBase
    {
      
        private readonly IDominoRepository _dominoChainRepository;
        public DominoFullGameController(IDominoRepository dominoChainRepository)
        {
            _dominoChainRepository = dominoChainRepository;
          
        }

        [HttpGet]

        public IEnumerable<DominoFullGame> GetDominoPieces()
        {
            var dominoPieces = _dominoChainRepository.GetDomino();

            return dominoPieces;
        }

        [HttpPost]

        public ActionResult CreateDominoFullGame(List<DominoDto> dominoChain)
        {

            if (dominoChain.Count < 2 || dominoChain.Count > 6)
            {
                return BadRequest("Debe ingresar entre 2 y 6 fichas");
            }

            var domino = _dominoChainRepository.CreateDominoFullGame(dominoChain);

            var dominoJson = JsonSerializer.Serialize(domino);

            if (domino == null)
            {
                
                return Ok("Las fichas ingresadas no son validas para craer un juego de dominó");
            }
            else
            {
               
                return Ok ("Las fichas ingresadas son validas para craer un juego de dominó \n" + dominoJson);
            }
            

        }
    }


}
