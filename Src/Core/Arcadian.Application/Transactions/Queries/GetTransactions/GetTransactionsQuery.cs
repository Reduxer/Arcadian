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

namespace Arcadian.Application.Transactions.Queries.GetTransactions
{
    public class GetTransactionsQuery : IRequest<TransactionsVM>
    {

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
            var transactions = await _dbContext.Transactions
                .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new TransactionsVM()
            {
                Data = transactions,
                Total = transactions.Count,
            };
        }
    }
}
