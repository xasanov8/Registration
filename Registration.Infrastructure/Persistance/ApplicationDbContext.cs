using Microsoft.EntityFrameworkCore;
using Registration.Domein.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
        
        }

        public DbSet<Login> Logins { get; set; }
        public DbSet<Code> Codes { get; set; }
    }
}
