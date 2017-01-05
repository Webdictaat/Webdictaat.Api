
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
using Webdictaat.CMS.Models;
using Webdictaat.Core;
using Webdictaat.Domain;
using Webdictaat.Domain.User;


namespace Webdictaat.CMS
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnv;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            _hostingEnv = env;

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Deze methode bevat alle middleware, de volgorde van de methodes zijn dus belangrijk!
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors(b => 
                b.WithOrigins("http://localhost:8080", "http://localhost:3000", "http://student.aii.avans.nl", "http://webdictaat.azurewebsites.net")
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseIdentity();


            //// Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
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
