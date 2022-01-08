using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MediatR;
using Api.Response;
using Application.DTO;
using Domain.ValueObjects;
using Application.Queries;
using Domain.Entities;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult<ApiResponse<User>>> GetUser(string email)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByEmail(email));
                if (user is not null)
                {
                return Ok(ApiResponse<User>.FromData(user));

                }
                return NotFound(ApiResponse<User>.WithError(Responses.UserNotFound));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<User>.WithError(ex.Message));

            }

        }



    }

}