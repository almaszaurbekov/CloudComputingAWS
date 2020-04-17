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

            CreateMap<Product, ProductJsonModel>(MemberList.Source);
            CreateMap<ProductJsonModel, Product>(MemberList.None);
        }
    }
}
