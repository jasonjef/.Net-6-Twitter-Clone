using App.Application.Dtos;
using App.Infrastructure.Data;
using AutoMapper;

namespace App.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<UserDto, User>();


        CreateMap<PostDto, Post>()         
            .ForMember(dto => dto.Content, src => src.MapFrom(t => t.Content))
            .ForPath(a => a.User.Name, s => s.MapFrom(t => t.Name))
            .ForPath(a => a.User.UserName, s => s.MapFrom(t => t.UserName))
            .ForPath(a => a.User.ProfileImagePath, s => s.MapFrom(t => t.ProfileImagePath))
            .ForPath(a => a.Reactions, a => a.MapFrom(src => src.ReactionTypeL))
            .ForPath(a => a.Reactions, a => a.MapFrom(src => src.ReactionTypeR));
    }
}