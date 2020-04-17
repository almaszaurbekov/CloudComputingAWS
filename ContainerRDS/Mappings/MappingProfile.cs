using AutoMapper;
using DataAccess.JsonModels;
using DataAccess.Models;

namespace ContainerRDS.Mappings
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserJsonModel>(MemberList.Source)
                .ForMember(d => d.IsSuccess, opt => opt.MapFrom(src => src.EmailConfirmed));
            CreateMap<UserJsonModel, User>(MemberList.None);
            CreateMap<User, User>(MemberList.Source);
        }
    }
}
