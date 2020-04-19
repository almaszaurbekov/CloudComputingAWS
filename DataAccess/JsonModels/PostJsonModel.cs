using DataAccess.JsonModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.JsonModels
{
    public class PostJsonModel : BaseJsonModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public PostJsonModel()
        {
            IsSuccess = true;
        }

        public PostJsonModel(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
    }
}
