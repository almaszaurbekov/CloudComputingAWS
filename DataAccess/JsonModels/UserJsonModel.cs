using DataAccess.JsonModels.Base;
namespace DataAccess.JsonModels
{
    public class UserJsonModel : BaseJsonModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public double Wallet { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }

        public UserJsonModel() { }

        public UserJsonModel(string error, bool isSuccess)
        {
            Error = error;
            IsSuccess = isSuccess;
        }
    }
}
