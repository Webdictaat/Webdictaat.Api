using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.Google;

namespace Webdictaat.Api.ViewModels
{
    public class PageAnalytics
    {
        private PageView pv;

        public PageAnalytics(PageView pv)
        {
            this.pv = pv;
        }

        public string PageName { get; set; }
    }
}
