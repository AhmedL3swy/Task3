using AutoMapper;
using DataGrid.Application.Features.Users.Command.AddUser;
using DataGrid.Application.Features.Users.Command.EditUser;
using DataGrid.Application.Features.Users.Query;
using DataGrid.Application.Shared.Models;
using DataGrid.Domain;
using System.Globalization;

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
            CreateMap<NewUserDto, User>()
                // Map from NewUserDto to User the string birthdate to DateOnly? BirthDate 
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.BirthDate, "dd/MM/yyyy"))).ReverseMap();
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.BirthDate, "dd/MM/yyyy"))).ReverseMap()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
