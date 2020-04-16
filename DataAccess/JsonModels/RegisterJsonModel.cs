using DataAccess.JsonModels.Base;
namespace DataAccess.JsonModels
{
    public class RegisterJsonModel : BaseJsonModel
    {
        public string Email { get; set; }
        public string Result { get; set; }

        public RegisterJsonModel() { }

        public RegisterJsonModel(string error, bool isSuccess)
        {
            Error = error;
            IsSuccess = isSuccess;
        }
    }
}