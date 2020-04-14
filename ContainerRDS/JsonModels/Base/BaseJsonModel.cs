using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContainerRDS.JsonModels.Base
{
    public class BaseJsonModel
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
    }
}
