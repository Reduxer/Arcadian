using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Arcadian.Application.Transactions.Queries.GetTransactions;
using Arcadian.Application.Transactions.Commands.CreateTransaction;
using Arcadian.Application.Transactions.Commands.UpdateTransaction;
using Arcadian.Application.Transactions.Commands.DeleteTransaction;

namespace Arcadian.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(TransactionsVM), StatusCodes.Status200OK)]
        public async Task<ActionResult<TransactionsVM>> Index()
        {
            return await Mediator.Send(new GetTransactionsQuery());
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(TransactionsVM), StatusCodes.Status200OK)]
        public async Task<ActionResult<TransactionsVM>> Index(Guid id)
        {
            return await Mediator.Send(new GetTransactionByIdQuery(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        public async Task<ActionResult<int>> Index(CreateTransactionCommand createTransaction)
        {
            return await Mediator.Send(createTransaction);
        }

        [HttpPut]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        public async Task<ActionResult<int>> Index(UpdateTransactionCommand updateTransaction)
        {
            return await Mediator.Send(updateTransaction);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        public async Task<ActionResult<int>> Index(DeleteTransactionCommand deleteTransaction)
        {
            return await Mediator.Send(deleteTransaction);
        }
    }
}
