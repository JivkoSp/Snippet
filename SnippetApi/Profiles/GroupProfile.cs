using AutoMapper;
using SnippetApi.Models;
using SnippetApi.Models.Dtos;

namespace SnippetApi.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, ReadGroupDto>();
            CreateMap<WriteGroupDto, Group>();
        }
    }
}
