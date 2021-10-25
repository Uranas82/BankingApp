using Contracts.Models.Request;
using Contracts.Models.Response;
using Domain.Clients.Firebase;
using Microsoft.AspNetCore.Mvc;
using Persistence.Models;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IFirebaseClient _firebaseClient;
        private readonly IUsersRepository _usersRepository;

        public AuthController(IFirebaseClient firebaseClient, IUsersRepository usersRepository)
        {
            _firebaseClient = firebaseClient;
            _usersRepository = usersRepository;
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<ActionResult<SignUpResponse>> SignUp(SignUpRequest request)
        {
            var user = await _firebaseClient.SignUpAsync(request.Email, request.Password);

            var userReadModel = new UserWriteModel
            {
                UserId = Guid.NewGuid(),
                FirebaseId = user.FirebaseId,
                Username = request.Username,
                Email = user.Email,
                DateCreated = DateTime.Now
            };

            await _usersRepository.SaveAsync(userReadModel);

            return Ok(new SignUpResponse
            {
                UserId = userReadModel.UserId,
                Email = userReadModel.Email,
                Username = userReadModel.Username,
                DateCreated = userReadModel.DateCreated
            });
        }

        [HttpPost]
        [Route("signIn")]
        public async Task<ActionResult<SignInResponse>> SignIn(SignInRequest request)
        {
            var user = await _firebaseClient.SignInAsync(request.Email, request.Password);

            var userReadModel = await _usersRepository.GetAsync(user.FirebaseId);

            return Ok(new SignInResponse
            {
                Username = userReadModel.Username,
                Email = user.Email,
                IdToken = user.IdToken
            });
        }
    }
}
