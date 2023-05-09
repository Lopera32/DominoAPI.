using Domino.Core.DTOs;
using Domino.Core.Entities;
using Domino.Core.Interfaces;
using Domino.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace Domino.Infrastructure.Repositories
{
    public class DominoFullGameRepository : IDominoRepository
    {
        private DominoContext _context;
        public DominoFullGameRepository(DominoContext context)
        {

            _context = context;
        }


        public IEnumerable<DominoFullGame> GetDomino()
        {
            var dominoPieces = _context.dominoFullGames.ToList();

            return dominoPieces;
        }

        public List<DominoDto> CreateDominoFullGame(List<DominoDto> dominoChain)
        {
            var domino = CalculateGameChain(dominoChain);

            var dominoJson = JsonSerializer.Serialize(domino);

            if (dominoJson == null)
            {
                var dominoGame = new DominoFullGame { DominoGame = dominoJson, isValid = false, UserId = 1 };
                _context.Add(dominoGame);
                _context.SaveChanges();
                return domino;
            }
            else
            {
                var dominoGame = new DominoFullGame { DominoGame = dominoJson, isValid = true, UserId = 1 };
                _context.Add(dominoGame);
                _context.SaveChanges();
                return domino;
            }


        }
        public List<DominoDto> CalculateGameChain(List<DominoDto> Piece)
        {
            var availablePieces = new List<DominoDto>(Piece);

            var dominoChain = new List<DominoDto> { availablePieces[0] };
            availablePieces.RemoveAt(0);

            while (availablePieces.Count > 0)
            {
                var matchFound = false;

                for (var i = 0; i < availablePieces.Count; i++)
                {
                    if (dominoChain[0].FirstValue == availablePieces[i].SecondValue)
                    {
                        dominoChain.Insert(0, availablePieces[i]);
                        availablePieces.RemoveAt(i);
                        matchFound = true;
                        break;
                    }
                    else if (dominoChain[0].FirstValue == availablePieces[i].FirstValue)
                    {
                        dominoChain.Insert(0, availablePieces[i]);
                        availablePieces.RemoveAt(i);
                        matchFound = true;
                        break;
                    }
                }

                if (matchFound)
                {
                    continue;
                }

                for (var i = 0; i < availablePieces.Count; i++)
                {
                    if (dominoChain[dominoChain.Count - 1].SecondValue == availablePieces[i].FirstValue)
                    {
                        dominoChain.Add(availablePieces[i]);
                        availablePieces.RemoveAt(i);
                        matchFound = true;
                        break;
                    }
                    else if (dominoChain[dominoChain.Count - 1].SecondValue == availablePieces[i].SecondValue)
                    {
                        dominoChain.Add(availablePieces[i]);
                        availablePieces.RemoveAt(i);
                        matchFound = true;
                        break;
                    }
                }

                if (!matchFound)
                {
                    return null;
                }
            }

            return dominoChain;


        }
    }
}



