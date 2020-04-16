using DataAccess.JsonModels.Base;
namespace DataAccess.JsonModels
{
    public class LoginJsonModel : BaseJsonModel
    {
        public string UserEmail { get; set; }

        public LoginJsonModel(bool isSuccess = false)
        {
            IsSuccess = isSuccess;
        }

        public LoginJsonModel(string error, bool isSuccess = false)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public LoginJsonModel(string email)
        {
            IsSuccess = true;
            UserEmail = email;
        }
    }
}
