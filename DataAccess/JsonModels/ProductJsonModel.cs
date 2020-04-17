using DataAccess.JsonModels.Base;
namespace DataAccess.JsonModels
{
    public class ProductJsonModel : BaseJsonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public ProductJsonModel()
        {
            IsSuccess = true;
        }

        public ProductJsonModel(string error, bool isSuccess)
        {
            Error = error;
            IsSuccess = isSuccess;
        }
    }
}
