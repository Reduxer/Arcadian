using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Arcadian.Application.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionCommandValidator : AbstractValidator<UpdateTransactionCommand>
    {
        public UpdateTransactionCommandValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty();

            RuleFor(t => t.TransactionName)
                .NotEmpty();

            RuleFor(t => t.Date)
                .NotEmpty();
        }
    }
}
