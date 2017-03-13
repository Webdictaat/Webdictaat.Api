using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain
{
    public interface IResource
    {
        ICollection<string> GetContributersIds();
    }
}
