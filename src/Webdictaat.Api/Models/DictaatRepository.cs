﻿using Microsoft.Extensions.Configuration;
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
using Webdictaat.Domain.Assignments;
using Webdictaat.Domain.Google;
using Microsoft.AspNetCore.Mvc;

namespace Webdictaat.Api.Models
{
    public interface IDictaatRepository
    {
        IEnumerable<ViewModels.DictaatSummary> GetDictaten(string userId = null, bool isAdmin = false);
        Task<ViewModels.Dictaat> getDictaat(string name);
        ViewModels.Session GetCurrentSession(string dictaatName, string userId = null);
        void CreateDictaat(string name, ApplicationUser user, string template);
        void DeleteRepo(string name);
        ViewModels.DictaatMarkings getMarkings(string name);
        IEnumerable<UserVM> GetContributers(string dictaatName);
        IEnumerable<UserVM> AddContributer(string dictaatName, string contributerEmail);
        ViewModels.Dictaat CopyDictaat(string dictaatName, DictaatForm form);
    }

    public class DictaatRepository : IDictaatRepository
    {
        private string _directoryRoot;
        private string _pagesDirectory;
        private string _templatesDirectory;
        private string _dictatenDirectory;
        private IGoogleAnalytics _analyticsRepo;
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
            IGoogleAnalytics analyticsRepo,
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
            _analyticsRepo = analyticsRepo;
            _directory = directory;
            _context = context;

            //best place to build the factory
            _dictaatFactory = new DictaatFactory(appSettings.Value, directory, file, json);
            _pathHelper = new PathHelper(appSettings.Value);

        }

        /// <summary>
        /// Extra constructor mainly created for unit testing purposes.
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="directory"></param>
        /// <param name="dictaatFactory"></param>
        /// <param name="context"></param>
        public DictaatRepository(
            IOptions<ConfigVariables> appSettings,
            IGoogleAnalytics analyticsRepo,
            IDictaatFactory dictaatFactory,
            WebdictaatContext context)
        {
            _directoryRoot = appSettings.Value.DictaatRoot;
            _pagesDirectory = appSettings.Value.PagesDirectory;
            _dictatenDirectory = appSettings.Value.DictatenDirectory;
            _templatesDirectory = appSettings.Value.TemplatesDirectory;
            _analyticsRepo = analyticsRepo;
            _context = context;
            _dictaatFactory = dictaatFactory;
            _pathHelper = new PathHelper(appSettings.Value);
        }

        public IEnumerable<ViewModels.DictaatSummary> GetDictaten(string userId = null, bool isAdmin = false)
        {
            var dictaatDetails = _context.DictaatDetails
                .Include(dd => dd.Contributers).ThenInclude(Contributer => Contributer.User)
                .Include(dd => dd.DictaatOwner)
                .ToList();

            var dictaatSummarys = dictaatDetails.Select(dd => new ViewModels.DictaatSummary(dd, userId)).ToList();

            if (isAdmin)
                dictaatSummarys.ForEach(dd => dd.CanEdit = true);

            return dictaatSummarys;
        }

        public async Task<ViewModels.Dictaat> getDictaat(string dictaatName)
        {
            //from DB
            var details = _context.DictaatDetails
                .Include("DictaatOwner")
                .FirstOrDefault(d => d.Name == dictaatName);

            //from file system
            Domain.Dictaat dictaat = _dictaatFactory.GetDictaat(dictaatName);

            //analytics
            var analytics = await _analyticsRepo.GetPageViews(dictaatName);

            return new ViewModels.Dictaat(dictaat, details, analytics);
                
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
                IsEnabled = false, //by default we don't show the dictaten
            };

            var dictaatSession = new DictaatSession()
            {
                DictaatDetailsId = name,
                StartedOn = DateTime.Now,
            };

            _context.DictaatSession.Add(dictaatSession);
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
                .OrderBy(a => a.Metadata)
                .Where(a => a.DictaatDetailsId == name)
                .ToList();

            var session = _context.DictaatSession
                .Include("Participants.User.AssignmentSubmissions")
                .FirstOrDefault(s => s.DictaatDetailsId == name && s.EndedOn == null);

            var participants = session.Participants.OrderBy(p => p.Group).ToList();

            return new DictaatMarkings(assignments, participants);

        }



        public IEnumerable<UserVM> GetContributers(string dictaatName)
        {
            var dictaat = _context.DictaatDetails
                 .Include("Contributers.User")
                 .Include("DictaatOwner")
                 .FirstOrDefault(d => d.Name == dictaatName);

            var contributers = dictaat.Contributers.Select(c => new UserVM(c.User)).ToList();
            contributers.Add(new UserVM(dictaat.DictaatOwner));

            return contributers;
        }

        public IEnumerable<UserVM> AddContributer(string dictaatName, string contributerEmail)
        {
            var dictaat = _context.DictaatDetails
                .Include("Contributers")
                .FirstOrDefault(d => d.Name == dictaatName);

            var user = _context.Users
                .FirstOrDefault(u => u.Email == contributerEmail);

            if(user == null)
                return null;

            dictaat.Contributers.Add(new DictaatContributer()
            {
                User = user
            });

            _context.SaveChanges();

            return this.GetContributers(dictaatName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public ViewModels.Dictaat CopyDictaat(string dictaatName, DictaatForm form)
        {
            var dictaat = _context.DictaatDetails
                .Include(dd => dd.Assignments)
                .Include(dd => dd.DictaatOwner)
                .Include("Polls.Options")
                .Include("Quizes.Questions.Question.Answers")
                .FirstOrDefault(d => d.Name == dictaatName);

            string newName = form.Name;

            var newDictaatDetails = new Domain.DictaatDetails();
            newDictaatDetails.Name = newName;
            newDictaatDetails.DictaatOwner = dictaat.DictaatOwner;

            //copy quizes
            dictaat.Quizes.ToList().ForEach(q => newDictaatDetails.Quizes.Add(q.Copy(newName)));

            //copy assignments
            dictaat.Assignments.Where(a => a.AssignmentType != AssignmentType.Quiz).ToList()
                .ForEach(a => newDictaatDetails.Assignments.Add(a.Copy(newName)));

            //copy polls
            dictaat.Polls.ToList()
                .ForEach(p => newDictaatDetails.Polls.Add(p.Copy()));

            //create session
            newDictaatDetails.Sessions.Add(new DictaatSession());

            //In the end: Save all the changes!
            _context.DictaatDetails.Add(newDictaatDetails);
            _context.SaveChanges();

            //copy files
            Domain.Dictaat newDictaat = _dictaatFactory
                .CopyDictaat(dictaatName, newDictaatDetails);


            return new ViewModels.Dictaat(newDictaat, newDictaatDetails, null);

        }
    }
}