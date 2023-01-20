using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arcadian.Domain.Entities;
using Ardalis.Specification;

namespace Arcadian.Application.Transactions.Queries.Specs
{
    public class TransactionBySearchStringSpec : Specification<Transaction>
    {
        public TransactionBySearchStringSpec(string? searchString)
        {
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                Query.Where((t) =>
                    t.TransactionId.ToString() == searchString
                        || t.TransactionName.Equals(searchString, StringComparison.OrdinalIgnoreCase)
                );

            }
        }
    }
}
