using DataAccess.JsonModels.Base;
using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.JsonModels
{
    public class OrderListJsonModel : BaseJsonModel
    {
        public List<Order> Orders { get; set; }

        public OrderListJsonModel() { }

        public OrderListJsonModel(List<Order> orders)
        {
            Orders = orders;
            IsSuccess = true;
        }

        public OrderListJsonModel(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
    }
}
