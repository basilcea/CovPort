using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Api.Extension;
using Domain.Entities;
using AutoMapper;
using System.Collections.Generic;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ApiController<User>
    {

        public UserController(IMediator mediator,  IMapper mapper): base(mediator, mapper)
        {
        }
        

        [HttpGet("{email}")]
        public Task<ActionResult<ApiResponse<IEnumerable<User>>>> GetUser(string email)
        {
            return Get(email: email);
        }

        [HttpGet("/summary")]
        public Task<ActionResult<ApiResponse<User>>> GetUserSummary(string userId)
        {
            return GetSummary(userId);
        }



    }

}