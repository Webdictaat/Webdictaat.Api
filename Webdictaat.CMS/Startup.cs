
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MVCWithAuth.Services;
using Webdictaat.CMS.Models;
using Webdictaat.Core;
using Webdictaat.Domain;
using Webdictaat.Domain.User;

namespace Webdictaat.CMS
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            //if (env.IsDevelopment())
            //{
            //    // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
            //    builder.AddUserSecrets<Startup>();
            //}

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

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<WebdictaatContext>()
                .AddDefaultTokenProviders();


            services.AddMvc();
            services.AddCors();
            services.AddOptions();

            services.AddSingleton<IDictaatRepository, DictaatRepository>();
            services.AddSingleton<IPageRepository, PageRepository>();
            services.AddSingleton<IMenuRepository, MenuRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddSingleton<IImageRepository, ImageRepository>();
            services.AddSingleton<Core.IDirectory, Core.Directory>();
            services.AddSingleton<Core.IFile, Core.File>();
            services.Configure<ConfigVariables>(Configuration.GetSection("ConfigVariables"));

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Deze methode bevat alle middleware, de volgorde van de methodes zijn dus belangrijk!
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseCors(b => b.WithOrigins("*").WithHeaders("*").WithMethods("*").WithExposedHeaders("*"));

            app.UseIdentity();

            //// Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            app.UseGoogleAuthentication(new GoogleOptions
            {
                ClientId = "1082440858387-eh3p4hakp02nbhvkb3sqr6ssgjksp5e5.apps.googleusercontent.com",
                ClientSecret = "sWXw2LY2y6d2ATCZuS6BLWMn",
            });

            app.UseMvc();
        }
    }
}
