using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using Webdictaat.Core;
using Microsoft.Extensions.Options;
using Webdictaat.Core.Helper;
using Webdictaat.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Webdictaat.Domain.User;
using Webdictaat.Data;
using Microsoft.EntityFrameworkCore;
using Webdictaat.Api;
using Webdictaat.Api.ViewModels;

namespace Webdictaat.Api.Models
{
    public interface IDictaatRepository
    {
        IEnumerable<ViewModels.DictaatSummary> GetDictaten(string userId = null);
        ViewModels.Dictaat getDictaat(string name);
        ViewModels.Session GetCurrentSession(string dictaatName, string userId = null);
        void CreateDictaat(string name, ApplicationUser user, string template);
        void DeleteRepo(string name);
        bool Join(string dictaatName, string userId);
        ViewModels.DictaatMarkings getMarkings(string name);
    }

    public class DictaatRepository : IDictaatRepository
    {
        private string _directoryRoot;
        private string _pagesDirectory;
        private string _templatesDirectory;
        private string _dictatenDirectory;

        private IDirectory _directory;
        private IDictaatFactory _dictaatFactory;

        private WebdictaatContext _context;


        private PathHelper _pathHelper;
        private UserManager<object> _userManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="directory"></param>
        /// <param name="dictaatFactory"></param>
        /// <param name="context"></param>
        public DictaatRepository(
            IOptions<ConfigVariables> appSettings, 
            IDirectory directory,
            IFile file,
            Core.IJson json,
            WebdictaatContext context)
        {
            _directoryRoot = appSettings.Value.DictaatRoot;
            _pagesDirectory = appSettings.Value.PagesDirectory;
            _dictatenDirectory = appSettings.Value.DictatenDirectory;
            _templatesDirectory = appSettings.Value.TemplatesDirectory;
            var menuConfigName = appSettings.Value.MenuConfigName;
            _directory = directory;
            _context = context;

            //best place to build the factory
            _dictaatFactory = new DictaatFactory(appSettings.Value, directory, file, json);
            _pathHelper = new PathHelper(appSettings.Value);

        }

        public IEnumerable<ViewModels.DictaatSummary> GetDictaten(string userId = null)
        {
            var dictaatDetails = _context.DictaatDetails
                .Include(dd => dd.Contributers).ThenInclude(Contributer => Contributer.User)
                .Include(dd => dd.DictaatOwner)
                .ToList();

            //var dictaatSummarys = _dictaatFactory.GetDictaten();

            return dictaatDetails.Select(dd => new ViewModels.DictaatSummary(dd, userId)).ToList();
        }

        public ViewModels.Dictaat getDictaat(string name)
        {
            string pagesPath = name + _pagesDirectory;
            Domain.Dictaat dictaat = _dictaatFactory.GetDictaat(name);
            return new ViewModels.Dictaat(dictaat);
                
        }

        /// <summary>
        /// Create a new dictaat based on a given template. 
        /// </summary>
        /// <param name="name">Name of the dictaat. Must be unique.</param>
        /// <param name="template">optional template name, default = defaultTemplate</param>
        public void CreateDictaat(string name, ApplicationUser user, string template)
        {
            //Create the database entry
            var dictaatDetails = new DictaatDetails()
            {
                Name = name,
                DictaatOwnerId = user.Id,
            };

            _context.DictaatDetails.Add(dictaatDetails);
            _context.SaveChanges();

            //Create the folder
            Domain.Dictaat dictaat = _dictaatFactory.CreateDictaat(name, template);
        }

        /// <summary>
        /// Delete a dictaat with given name. 
        /// </summary>
        /// <param name="name"></param>
        public void DeleteRepo(string name)
        {
            string dictaatPath = _pathHelper.DictaatPath(name);
            _dictaatFactory.DeleteDictaaat(dictaatPath);

            _context.DictaatDetails.Remove(_context.DictaatDetails.FirstOrDefault(dd => dd.Name == name));
            _context.SaveChanges();
        }

        public bool Join(string dictaatName, string userId)
        {

            var currentSession = _context.DictaatSession
                .Include(s => s.Participants)
                .FirstOrDefault(s => s.EndedOn == null && dictaatName == dictaatName);

            if(currentSession.Participants.Any(p => p.UserId == userId))
            {
                return false; //already in the partcipant list
            }
            else
            {
                currentSession.Participants.Add(new DictaatSessionUser()
                {
                    UserId = userId
                });
                _context.SaveChanges();
                return true; //Joined this ditaat :D
            }


        }

        public ViewModels.Session GetCurrentSession(string dictaatName, string userId = null)
        {
            var count = this._context.DictaatSession
                .Include(ds => ds.DictaatDetails)
                .Where(s => s.DictaatDetailsId == dictaatName).Count();

            if(count == 0) {
                var s = new DictaatSession()
                {
                    DictaatDetailsId = dictaatName,
                    StartedOn = DateTime.Now,
                };
                _context.DictaatSession.Add(s);
                _context.SaveChanges();
            }

            var session = _context.DictaatSession
                .Include("Participants.User")
                .FirstOrDefault(s => s.DictaatDetailsId == dictaatName && s.EndedOn == null);

            var response = new ViewModels.Session(session);
            if(userId != null && response.ParticipantIds.Contains(userId))
            {
                response.ContainsMe = true;
            }
            return response;
        }

        public DictaatMarkings getMarkings(string name)
        {
            var assignments = _context.Assignments
                .Include(a => a.Attempts)
                .Where(a => a.DictaatDetailsId == name)
                .ToList();

            var participants = _context.DictaatSession
                .Include("Participants.User")
                .FirstOrDefault(s => s.DictaatDetailsId == name && s.EndedOn == null)
                .Participants.Select(p => p.User);

            return new DictaatMarkings(assignments, participants);

        }
    }
}