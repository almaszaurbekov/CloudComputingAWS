using DataAccess.JsonModels.Base;
using DataAccess.Models;
using System.Collections.Generic;
namespace DataAccess.JsonModels
{
    public class ProductListJsonModel : BaseJsonModel
    {
        public List<Product> Products { get; set; }

        public ProductListJsonModel() { }

        public ProductListJsonModel(List<Product> products)
        {
            Products = products;
            IsSuccess = true;
        }

        public ProductListJsonModel(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
    }
}
