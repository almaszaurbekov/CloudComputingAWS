using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public double Wallet { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
    }
}
