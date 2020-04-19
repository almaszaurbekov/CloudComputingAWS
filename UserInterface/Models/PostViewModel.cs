using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class PostViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
