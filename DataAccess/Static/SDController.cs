using DataAccess.Models;
using System;
using System.Collections.Generic;
namespace DataAccess.Static
{
    public class SDHelper
    {
        public static bool IsValueNotNull(string value) => !string.IsNullOrEmpty(value);
    }
}