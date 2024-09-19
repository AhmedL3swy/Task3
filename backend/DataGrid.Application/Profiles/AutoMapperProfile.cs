using AutoMapper;
using DataGrid.Application.Features.Users.Query;
using DataGrid.Application.Shared.Models;
using DataGrid.Domain;

namespace DataGrid.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserResultDto>().
                ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.MaritalStatus.Name)).ReverseMap();
            CreateMap<SearchResult<User>, SearchResult<UserResultDto>>().
                ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data)).ReverseMap();


        }

    }
}
