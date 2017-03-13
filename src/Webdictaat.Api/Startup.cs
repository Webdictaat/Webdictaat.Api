
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using MVCWithAuth.Services;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Swagger.Model;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Webdictaat.Api.Services;
using Webdictaat.CMS.Models;
using Webdictaat.Core;
using Webdictaat.Data;
using Webdictaat.Domain;
using Webdictaat.Domain.User;


namespace Webdictaat.CMS
{
    /// <summary>
    /// Default dotnet core class to use for startup configuration
    /// </summary>
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnv;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            _hostingEnv = env;



            if (env.IsDevelopment())
            {
                //As a best practice, it is not recommended to store the secrets in a configuration file in the 
                //application since they can be checked into source control which may be publicly accessible.+
                //The SecretManager tool stores sensitive application settings in the user profile folder on the local 
                //machine. These settings are then seamlessly merged with settings from all other
                //sources during application startup.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<WebdictaatContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                            ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        return Task.FromResult(0);
                    }
                };
            })
                .AddEntityFrameworkStores<WebdictaatContext>()
                .AddDefaultTokenProviders();


            services.AddCors();
            services.AddOptions();
            services.AddMvc();


            //Swagger
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Webdictaat API",
                    Description = "API for dictaten",
                    TermsOfService = "None"
                });
                options.DescribeAllEnumsAsStrings();
            });

            #region custom services
            services.AddSingleton<IAuthorizeService, AuthorizeService>();
            services.AddSingleton<IDictaatRepository, DictaatRepository>();
            services.AddSingleton<IPageRepository, PageRepository>();
            services.AddSingleton<IMenuRepository, MenuRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddSingleton<IImageRepository, ImageRepository>();
            services.AddSingleton<Core.IDirectory, Core.Directory>();
            services.AddSingleton<Core.IFile, Core.File>();
            IConfigurationSection config = Configuration.GetSection("ConfigVariables");
            config["DictaatRoot"] = _hostingEnv.WebRootPath;
            services.Configure<ConfigVariables>(config);
            #endregion

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// Deze methode bevat alle middleware, de volgorde van de methodes zijn dus belangrijk!
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors(b => b
                .AllowAnyOrigin()
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseIdentity();

            //// Add external authentication middleware below. 
            //To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            //The client Id and ClientSecret are stored in the secret manager. 
            app.UseGoogleAuthentication(new GoogleOptions
            {
                ClientId = Configuration.GetSection("IdentityProviders:Google:ClientId").Value,
                ClientSecret = Configuration.GetSection("IdentityProviders:Google:ClientSecret").Value,
            });

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi();

        }

        private string GetXmlCommentsPath(ApplicationEnvironment appEnvironment)
        {
            return Path.Combine(appEnvironment.ApplicationBasePath, "Webdictaat.CMS.xml");
        }
    }
}
