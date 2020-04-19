using DataAccess.JsonModels.Base;
namespace DataAccess.JsonModels
{
    public class OrderJsonModel : BaseJsonModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public double TotalPrice { get { return ProductPrice * ProductCount; } }

        public OrderJsonModel()
        {
            IsSuccess = true;
        }

        public OrderJsonModel(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
    }
}
