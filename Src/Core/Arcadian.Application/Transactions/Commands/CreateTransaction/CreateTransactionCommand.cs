using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Arcadian.Application.Common.Interfaces.Contexts;
using Arcadian.Domain.Entities;

namespace Arcadian.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<int>
    {
        public string TransactionName { get; set; } = default!;

        public decimal Cost { get; set; }

        public DateTime Date { get; set; }
    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
    {
        private readonly IDatabaseContext _dbContext;

        public CreateTransactionCommandHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new Transaction()
            {
                TransactionName = request.TransactionName,
                Cost= request.Cost,
                Date = request.Date.ToUniversalTime(),
            };

            _dbContext.Transactions.Add(transaction);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

}
