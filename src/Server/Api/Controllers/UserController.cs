using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Api.Extension;
using Domain.Entities;
using AutoMapper;
using System.Collections.Generic;
using Domain.Aggregates;
using System;
using Application.DTO;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ApiController<User>
    {

        public UserController(IMediator mediator,  IMapper mapper): base(mediator, mapper)
        {
        }
        

        [HttpGet]
        public Task<ActionResult<ApiResponse<IEnumerable<User>>>> GetUser(UserRequestBody body)
        {
            return Get(body.Email);
        }

        [HttpGet("summary")]
        public Task<ActionResult<ApiResponse<IEnumerable<UserSummary>>>> GetUserSummary([FromQuery] string userId)
        {
            return GetSummary<UserSummary>(userId);
        }



    }

}