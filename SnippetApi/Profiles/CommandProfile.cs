using AutoMapper;
using SnippetApi.Models;
using SnippetApi.Models.Dtos;

namespace SnippetApi.Profiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            CreateMap<Command, ReadCommadDto>();
            CreateMap<WriteCommandDto, Command>();
            CreateMap<Command, UpdateCommandDto>();
            CreateMap<UpdateCommandDto, Command>();
        }
    }
}
