using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Arcadian.Domain.Entities;

namespace Arcadian.Application.Transactions.Queries.Specs
{
    public class TransactionByIdSpec : Specification<Transaction>, ISingleResultSpecification<Transaction>
    {
        public TransactionByIdSpec(Guid id)
        {
            Query.Where(t => t.TransactionId == id);
        }
    }
}
