
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.Swagger.Model;
using System;
using System.Text;
using Webdictaat.Api.Models;
using Webdictaat.Api.Services;
using Webdictaat.Core;
using Webdictaat.Data;
using Webdictaat.Domain.Google;
using Webdictaat.Domain.User;

namespace Webdictaat.Api
{
    /// <summary>
    /// Default dotnet core class to use for startup configuration
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        private IHostingEnvironment _hostingEnv;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnv)
        {
            Configuration = configuration;
            _hostingEnv = hostingEnv;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();

            services.AddDbContext<WebdictaatContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //GOOGLE LOGIN
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = Configuration.GetSection("IdentityProviders:Google:ClientId").Value;
                options.ClientSecret = Configuration.GetSection("IdentityProviders:Google:ClientSecret").Value;
            });

            //CORS & MVC  - Volgorde is belangrijk!
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<WebdictaatContext>();

            //JSON WEB TOKEN
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            }).AddJwtBearer("Jwt", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256")),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

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

            //prepare
            var gooleServiceAccountConfig = Configuration.GetSection("IdentityProviders:GoogleServiceAccount").Value;
  

            #region custom services
            services.AddScoped<IAuthorizeService, AuthorizeService>();
            services.AddScoped<IDictaatRepository, DictaatRepository>();
            services.AddScoped<IPageRepository, PageRepository>();     
            services.AddScoped<IStyleRepository, StyleRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<Core.IDirectory, Core.Directory>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<IGoogleAnalytics, GoogleAnalytics>((sp) => new GoogleAnalytics(gooleServiceAccountConfig));
            services.AddScoped<ISecretService, SecretService>();
            services.AddScoped<Core.IFile, Core.File>();
            services.AddScoped<IAchievementRepository, AchievementRepository>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<IPollRepository, PollRepository>();
            services.AddScoped<Core.IJson, Core.Json>();

            IConfigurationSection config = Configuration.GetSection("ConfigVariables");
            services.Configure<ConfigVariables>(config);
            config["DictaatRoot"] = _hostingEnv.WebRootPath;
            #endregion

            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// Deze methode bevat alle middleware, de volgorde van de methodes zijn dus belangrijk!
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, WebdictaatContext db)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors(b => b
                .AllowAnyOrigin()
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            app.UseSwagger();
            app.UseSwaggerUi();

            app.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Page not found");
            });

            //migrate db
            db.Database.Migrate();

        }
    }
}
