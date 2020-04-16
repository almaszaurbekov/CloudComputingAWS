using DataAccess.JsonModels.Base;
using DataAccess.Models;
using System.Collections.Generic;
namespace DataAccess.JsonModels
{
    public class UserListJsonModel : BaseJsonModel
    {
        public List<User> Users { get; set; }

        public UserListJsonModel(List<User> users)
        {
            Users = users;
            IsSuccess = true;
        }

        public UserListJsonModel(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
    }
}
