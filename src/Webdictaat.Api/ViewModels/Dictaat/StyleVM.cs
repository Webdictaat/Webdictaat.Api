using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Api.ViewModels
{
    /// <summary>
    /// A small VM for updating the content of a stylesheet.
    /// </summary>
    public class StyleVM
    {
        /// <summary>
        /// The content of the stylesheet
        /// </summary>
        public string Content { get; set; }
    }
}
