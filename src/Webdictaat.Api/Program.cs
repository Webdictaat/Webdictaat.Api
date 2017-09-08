using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

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
            var host = BuildWebHost(args);
            host.Run();
        }

        /// <summary>
        /// Upgraded EF core from 1.* to 2.0
        /// https://github.com/aspnet/Announcements/issues/258
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        // Tools will use this to get application services
        public static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}
