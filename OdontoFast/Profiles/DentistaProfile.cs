using AutoMapper; // Importa a biblioteca AutoMapper para mapeamento de objetos
using OdontoFast.DTOs; // Importa os DTOs utilizados para o mapeamento
using OdontoFast.Models; // Importa os modelos que representam as entidades

namespace OdontoFast.Profiles
{
    // Classe de perfil do AutoMapper para mapeamento entre as entidades Dentista e seus respectivos DTOs
    public class DentistaProfile : Profile
    {
        // Construtor que configura os mapeamentos
        public DentistaProfile()
        {
            // Mapeia a entidade Dentista para o DTO DentistaDto
            CreateMap<Dentista, DentistaDto>();

            // Mapeia o DTO CreateDentistaDto para a entidade Dentista
            CreateMap<CreateDentistaDto, Dentista>();

            // Mapeia o DTO UpdateDentistaDto para a entidade Dentista
            CreateMap<UpdateDentistaDto, Dentista>()
                // Configura a condição para que os membros sejam mapeados apenas se não forem nulos
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
