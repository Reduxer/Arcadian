using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Arcadian.Application.Common.Interfaces.Contexts;
using Arcadian.Domain.Entities;

namespace Arcadian.Data.Contexts
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public DatabaseContext() {}

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        
    }
}
