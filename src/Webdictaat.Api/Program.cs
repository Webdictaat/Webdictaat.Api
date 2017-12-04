using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore;

namespace Webdictaat.Api
{
    /// <summary>
    /// default C# class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// default startpoint C# application
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Upgraded EF core from 1.* to 2.0
        /// https://github.com/aspnet/Announcements/issues/258
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        // Tools will use this to get application services
        public static IWebHost BuildWebHost(string[] args) =>
                   WebHost.CreateDefaultBuilder(args)
                       .UseStartup<Startup>()
                       .Build();
    }
}
