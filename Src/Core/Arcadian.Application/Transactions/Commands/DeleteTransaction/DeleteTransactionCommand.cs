using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Arcadian.Application.Common.Exceptions;
using Arcadian.Application.Common.Interfaces.Contexts;
using Arcadian.Application.Transactions.Queries.Specs;
using Arcadian.Domain.Entities;
using Ardalis.Specification.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Arcadian.Application.Transactions.Commands.DeleteTransaction
{
    public class DeleteTransactionCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }

    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, int>
    {
        private readonly IDatabaseContext _dbContext;

        public DeleteTransactionCommandHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
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

            _dbContext.Transactions.Entry(transaction).State = EntityState.Deleted;
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
