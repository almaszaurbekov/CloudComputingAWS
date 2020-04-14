using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Services.Base;
namespace DataAccess.Services
{
    public interface IUserService : IService<User> { }

    public class UserService : EntityService<User>, IUserService
    {
        public UserService(RdsContext context) : base(context) { }
    }
}
