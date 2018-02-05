using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Api.ViewModels
{
    public class DictaatPageForm
    {
        public DictaatPageSummary Page { get; set; }

        public string SubMenu { get; set; }

        public string TemplateName { get; set; }
    }
}
