using AutoMapper;
using Domino.Core.DTOs;
using Domino.Core.Entities;

namespace Domino.Infrastructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DominoFullGameDto, DominoFullGame>();
            CreateMap<DominoFullGame, DominoFullGameDto>();

        }
    }
}
