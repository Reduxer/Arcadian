using System;
using System.Threading;
using System.Threading.Tasks;
using Arcadian.Application.Common.Exceptions;
using Arcadian.Application.Common.Interfaces.Contexts;
using Arcadian.Application.Transactions.Queries.Specs;
using Arcadian.Domain.Entities;
using Ardalis.Specification.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Arcadian.Application.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionCommand : IRequest<int>
    {
        public Guid Id { get; set; }

        public string TransactionName { get; set; } = default!;

        public decimal Cost { get; set; }

        public DateTime Date { get; set; }
    }

    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, int>
    {
        private readonly IDatabaseContext _dbContext;

        public UpdateTransactionCommandHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transactionByIdSpec = new TransactionByIdSpec(request.Id);

            var transaction = await _dbContext.Transactions
                .WithSpecification(transactionByIdSpec)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
                
            if(transaction is null)
            {
                throw new NotFoundException(nameof(Transaction), request.Id);
            }

            transaction.TransactionName = request.TransactionName;
            transaction.Cost = request.Cost;
            transaction.Date = request.Date.ToUniversalTime();

            _dbContext.Transactions.Update(transaction);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
