using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webdictaat.CMS.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Webdictaat.CMS.Controllers
{
    [Route("api/[controller]")]
    public class DictatenController : Controller
    {
        private IDictaatRepository _dictaatRepo;

        public DictatenController(IDictaatRepository dictaatRepo)
        {
            _dictaatRepo = dictaatRepo;
        }

        /// <summary>
        /// Returns a list of small summaries of webdictaten 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<ViewModels.DictaatSummary> Post([FromBody]ViewModels.DictaatForm form)
        {
            _dictaatRepo.CreateDictaat(form.Name, form.Template);
            return this.Get();
        }

        /// <summary>
        /// Returns a list of small summaries of webdictaten 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ViewModels.DictaatSummary> Get()
        {
            var dictaten = _dictaatRepo.GetDictaten();
            return dictaten;
        }
    
        /// <summary>
        /// Returns a detailed summary of 1 webdictaat
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public ViewModels.Dictaat Get(string name)
        {
            return _dictaatRepo.getDictaat(name);
        }

    }
}
