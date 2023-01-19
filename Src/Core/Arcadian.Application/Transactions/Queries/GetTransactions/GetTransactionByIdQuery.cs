using Arcadian.Application.Common.Interfaces.Contexts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Arcadian.Application.Transactions.Queries.Specs;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Arcadian.Application.Dtos.Transaction;
using Microsoft.EntityFrameworkCore;
using Arcadian.Application.Common.Exceptions;
using Arcadian.Domain.Entities;

namespace Arcadian.Application.Transactions.Queries.GetTransactions
{
    public class GetTransactionByIdQuery : IRequest<TransactionsVM>
    {
        public Guid Id { get; set; }

        public GetTransactionByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionsVM>
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public GetTransactionByIdQueryHandler(IDatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TransactionsVM> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var transactionByIdSpec = new TransactionByIdSpec(request.Id);

            var transaction = await _dbContext.Transactions
                .WithSpecification(transactionByIdSpec)
                .FirstOrDefaultAsync(cancellationToken);

            if(transaction is null)
            {
                throw new NotFoundException(nameof(Transaction), request.Id);
            }

            return new TransactionsVM()
            {
                Total = 1,
                Data = new List<TransactionDto>() { _mapper.Map<TransactionDto>(transaction) }
            };
        }
    }
}
