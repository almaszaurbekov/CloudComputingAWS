using AutoMapper;
using DataAccess.JsonModels;
using UserInterface.Models;
namespace UserInterface.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserJsonModel, UserViewModel>(MemberList.Source);
            CreateMap<UserJsonModel, UserViewModel>(MemberList.None);
        }
    }
}
