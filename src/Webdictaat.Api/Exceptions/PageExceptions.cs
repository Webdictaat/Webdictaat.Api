using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.CMS.Exceptions
{
    public class PageAlreadyExcistsException : Exception
    {
        public PageAlreadyExcistsException() : base("Trying to create a page that already excists.")
        {

        }
    }

    internal class PageNotFoundException : Exception
    {
        public PageNotFoundException()
            :base("Trying to get a page that does not excist.")
        {
        }

    }
}
