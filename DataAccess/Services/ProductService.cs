using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Services.Base;
namespace DataAccess.Services
{
    public interface IProductService : IService<Product> { }

    public class ProductService : EntityService<Product>, IProductService
    {
        public ProductService(RdsContext context) : base(context) { }
    }
}
