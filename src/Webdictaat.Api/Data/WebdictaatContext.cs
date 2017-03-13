using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.User;

namespace Webdictaat.Domain
{
    public class WebdictaatContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Question> Questions { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<DictaatDetails> DictaatDetails { get; set; }

        public WebdictaatContext(DbContextOptions<WebdictaatContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}
