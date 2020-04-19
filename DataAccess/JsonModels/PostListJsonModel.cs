using DataAccess.JsonModels.Base;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.JsonModels
{
    public class PostListJsonModel : BaseJsonModel
    {
        public List<Post> Posts { get; set; }

        public PostListJsonModel(List<Post> posts)
        {
            Posts = posts;
            IsSuccess = true;
        }

        public PostListJsonModel(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
    }
}
