using System.Threading.Tasks;
using Api.Extension;
using AutoMapper;
using Domain.Aggregates;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using System.Collections.Generic;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResultController : ApiController<Result>
    {
        public ResultController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet("{id}")]
        public Task<ActionResult<ApiResponse<Result>>> GetResult(string testId)
        {
            return GetById(testId);
        }

        [HttpGet]
        public Task<ActionResult<ApiResponse<IEnumerable<Result>>>> GetResults([FromQuery] string status)
        {
            return Get(status: status);
        }

        [HttpPost]
        public Task<ActionResult<ApiResponse<Result>>> CreateResult(ResultRequestBody request)
        {
            return Create<ResultRequestBody>(request);
        }

        [HttpPatch]
        public Task<ActionResult<ApiResponse<Result>>> UpdateResult(ResultRequestBody request)
        {
           return Update<ResultRequestBody>(request);
        }

        [HttpGet("/summary")]
        public Task<ActionResult<ApiResponse<ResultSummary>>> GetSummary()
        {
            return GetSummary();
        }



    }

}
