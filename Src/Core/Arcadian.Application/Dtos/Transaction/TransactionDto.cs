using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcadian.Application.Dtos.Transaction
{
    public class TransactionDto
    {
        public Guid TransactionId { get; set; }

        public string TransactionName { get; set; } = default!;

        public decimal Cost { get; set; }

        public DateTime Date { get; set; }
    }
}
