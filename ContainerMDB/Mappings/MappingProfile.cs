using AutoMapper;
using DataAccess.JsonModels;
using DataAccess.Models;

namespace ContainerMDB.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderJsonModel>();
            CreateMap<OrderJsonModel, Order>();

            CreateMap<Post, PostJsonModel>();
            CreateMap<PostJsonModel, Post>();
        }
    }
}
