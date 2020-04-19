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

            CreateMap<Product, ProductViewModel>(MemberList.Source);
            CreateMap<ProductViewModel, Product>(MemberList.None);

            CreateMap<ProductJsonModel, ProductViewModel>(MemberList.Source);
            CreateMap<ProductViewModel, ProductJsonModel>(MemberList.None);

            CreateMap<Post, PostViewModel>(MemberList.Source);
            CreateMap<PostViewModel, Post>(MemberList.None);

            CreateMap<PostJsonModel, PostViewModel>(MemberList.Source);
            CreateMap<PostViewModel, PostJsonModel>(MemberList.None);

            CreateMap<Order, OrderViewModel>(MemberList.Source);
            CreateMap<OrderViewModel, Order>(MemberList.None);

            CreateMap<OrderJsonModel, OrderViewModel>(MemberList.Source);
            CreateMap<OrderViewModel, OrderJsonModel>(MemberList.None);
        }
    }
}
