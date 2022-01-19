using AutoMapper;
using WebApiAuthores.Controllers.Entidades;
using WebApiAuthores.DTOs;

namespace WebApiAuthores.Utilidades
{
    public class AutoMapperDbContext : Profile
    {
        public AutoMapperDbContext()
        {
            CreateMap<ComentarioCreacionDTO, Comentario>();
        }
    }
}
