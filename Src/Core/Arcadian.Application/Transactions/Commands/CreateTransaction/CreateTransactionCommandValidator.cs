using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;


namespace Arcadian.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(t => t.TransactionName)
                .NotEmpty()
                .WithMessage("Transaction name is required");

            RuleFor(t => t.Date)
                .NotEmpty()
                .WithMessage("Date is required");
                
        }
    }
}
