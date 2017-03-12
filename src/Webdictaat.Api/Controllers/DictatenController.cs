using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webdictaat.CMS.Models;
using Microsoft.AspNetCore.Authorization;

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
        /// Authorized (Requires the user to be logged in.)
        /// Returns a detailed summary of 1 webdictaat
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        [Authorize]
        public ViewModels.Dictaat Get(string name)
        {
            return _dictaatRepo.getDictaat(name);
        }

        /// <summary>
        /// Authorized (Requires the user to be logged in)
        /// 
        /// </summary>
        /// <param name="name">Name of the dictaat to be deleted</param>
        /// <returns>Returns success or fail (true of false)</returns>
        [HttpDelete("{name}")]
        [Authorize]
        public bool Delete(string name)
        {
            //Nog niet goed nagedacht over wat er fout kan gaan bij het deleten.
            //Dus nu maar even op een vieze manier goed of fout checken
            try
            {
                _dictaatRepo.deleteRepo(name);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
