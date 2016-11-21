using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Webdictaat.CMS.Models;

using Webdictaat.Core;
using Webdictaat.Domain;

namespace Webdictaat.CMS
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            services.AddOptions();
            services.AddMvc();
            services.AddCors();
            services.AddOptions();

            services.AddSingleton<IDictaatRepository, DictaatRepository>();
            services.AddSingleton<IPageRepository, PageRepository>();
            services.AddSingleton<IMenuRepository, MenuRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();

            services.AddSingleton<Core.IDirectory, Core.Directory>();
            services.AddSingleton<Core.IFile, Core.File>();
            services.Configure<ConfigVariables>(Configuration.GetSection("ConfigVariables"));

            var connection = @"Server = Stijn; Database = Webdictaat; Trusted_Connection = True; MultipleActiveResultSets=True";
            services.AddDbContext<DomainContext>(options => options.UseSqlServer(connection), ServiceLifetime.Scoped);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            
            app.UseCors(b => b.WithOrigins("*").WithHeaders("*").WithMethods("*").WithExposedHeaders("*"));
            app.UseMvc();
        }
    }
}
