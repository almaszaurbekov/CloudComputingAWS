using DataAccess.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Post : MdbModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
