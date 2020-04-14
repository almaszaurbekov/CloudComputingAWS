using ContainerRDS.JsonModels.Base;
namespace ContainerRDS.JsonModels
{
    public class UserJsonModel : BaseJsonModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public UserJsonModel() { }

        public UserJsonModel(string error, bool isSuccess)
        {
            Error = error;
            IsSuccess = isSuccess;
        }
    }
}
