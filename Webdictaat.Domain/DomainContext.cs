using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain
{
    public class DomainContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }

        public DomainContext(DbContextOptions<DomainContext> options) : base(options)
        {
        }

    }
}
