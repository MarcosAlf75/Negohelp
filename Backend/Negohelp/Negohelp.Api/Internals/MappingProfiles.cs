using AutoMapper;
using Negohelp.Comun.Models;

namespace Negohelp.Api.Controllers
{
	internal class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<ClientePersona, ClientePersonaDto>().ReverseMap();
			CreateMap<ClienteEmpresa, ClienteEmpresaDto>().ReverseMap();
        }
    }
}
