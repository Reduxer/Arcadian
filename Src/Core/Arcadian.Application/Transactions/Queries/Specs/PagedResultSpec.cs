using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arcadian.Domain.Entities;
using Ardalis.Specification;

namespace Arcadian.Application.Transactions.Queries.Specs
{
    public class PagedResultSpec<T> : Specification<T>
    {
        public PagedResultSpec(int pageIndex, int pageSize)
        {
            Query.Skip(pageIndex * pageSize)
                .Take(pageSize);
        }
    }
}
