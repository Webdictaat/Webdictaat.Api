using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Webdictaat.Core.Extensions
{
    public static class StringExtensions
    {
        public static object ToJson(this string str)
        {
            return JsonConvert.DeserializeObject(str);
        }
    }
}
