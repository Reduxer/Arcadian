using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arcadian.Application.Dtos.Transaction;
namespace Arcadian.Application.Transactions.Queries.GetTransactions
{
    public class TransactionsVM
    {
        public int Total  { get; set; }

        public List<TransactionDto> Data { get; set; } = default!;
    }
}
