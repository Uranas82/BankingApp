using Contracts.Models.Request;
using Contracts.Models.Response;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ITransactionService _transactionService;


        public TransactionController(ITransactionsRepository transactionsRepository, IUsersRepository usersRepository, ITransactionService transactionService)
        {
            _transactionsRepository = transactionsRepository;
            _usersRepository = usersRepository;
            _transactionService = transactionService;
        }

        [Authorize]
        [HttpPost]
        [Route("topUp")]
        public async Task<ActionResult<TransactionResponse>> TopUp(TransactionRequest request, string firebaseId)
        {
            firebaseId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;
            try
            {
                var response = await _transactionService.TopUp(request, firebaseId);

                return response;
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
