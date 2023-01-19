using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Arcadian.Domain.Entities;
using System.Threading;

namespace Arcadian.Application.Common.Interfaces.Contexts
{
    public interface IDatabaseContext
    {
        DbSet<Transaction> Transactions { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
