using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper.QueryableExtensions;
using Arcadian.Application.Common.Interfaces.Contexts;
using AutoMapper;
using Arcadian.Application.Dtos.Transaction;
using Microsoft.EntityFrameworkCore;
using Ardalis.Specification.EntityFrameworkCore;
using Arcadian.Application.Transactions.Queries.Specs;
using Arcadian.Domain.Entities;

namespace Arcadian.Application.Transactions.Queries.GetTransactions
{
    public class GetTransactionsQuery : IRequest<TransactionsVM>
    {
        public string? SearchString { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }

    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, TransactionsVM>
    {
        private readonly IDatabaseContext _dbContext;
        private IMapper _mapper;

        public GetTransactionsQueryHandler(IDatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TransactionsVM> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactionBySearchStringSpec = new TransactionBySearchStringSpec(request.SearchString);

            var pagedResultSpec = new PagedResultSpec<Transaction>(request.PageIndex, request.PageSize);

            var transactionsQuery = _dbContext.Transactions
                .WithSpecification(transactionBySearchStringSpec);

            var totalResult = await transactionsQuery
                .CountAsync(cancellationToken);

            var results = await transactionsQuery
                .WithSpecification(pagedResultSpec)
                .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new TransactionsVM()
            {
                Data = results,
                Total = totalResult,
            };
        }
    }
}
