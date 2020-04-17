using DataAccess.Models.Base;
namespace DataAccess.Models
{
    public class Order : MdbModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public double TotalPrice { get { return ProductPrice * ProductCount; }}
    }
}