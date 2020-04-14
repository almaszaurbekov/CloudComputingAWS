using ContainerRDS.JsonModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContainerRDS.JsonModels
{
    public class RegisterJsonModel : BaseJsonModel
    {
        public string Email { get; set; }
        public string Result { get; set; }

        public RegisterJsonModel() { }

        public RegisterJsonModel(string error, bool isSuccess)
        {
            Error = error;
            IsSuccess = isSuccess;
        }
    }
}
