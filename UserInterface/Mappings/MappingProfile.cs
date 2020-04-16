using AutoMapper;
using DataAccess.JsonModels;
using DataAccess.Models;
using UserInterface.Models;
namespace UserInterface.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserJsonModel, UserViewModel>(MemberList.Source);
            CreateMap<UserViewModel, UserJsonModel>(MemberList.None);

            CreateMap<User, UserViewModel>(MemberList.Source);
            CreateMap<UserViewModel, User>(MemberList.None);
        }
    }
}
