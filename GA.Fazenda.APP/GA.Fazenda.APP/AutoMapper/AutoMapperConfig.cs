using AutoMapper;
using GA.Fazenda.APP.ViewModels;
using GA.Fazenda.Business.Models;

namespace GA.Fazenda.APP.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<EntidadeFazenda, FazendaVM>().ReverseMap();
            CreateMap<Animal, AnimalVM>().ReverseMap();
        }
    }
}
