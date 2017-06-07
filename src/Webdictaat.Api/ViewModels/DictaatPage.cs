using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class DictaatPage : DictaatPageSummary
    {

        public string Source { get; set; }
    }
}
