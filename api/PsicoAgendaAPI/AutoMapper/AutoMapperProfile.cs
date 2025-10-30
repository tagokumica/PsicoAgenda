using AutoMapper;
using Domain.Entities;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Address, AddressViewModel>()
             .ForMember(c => c.UsersViewModel, b => b.MapFrom(v => v.Users))
             .ForMember(c => c.LocationsViewModel, b => b.MapFrom(v => v.Locations))
             .ReverseMap();

        }
    }
}
