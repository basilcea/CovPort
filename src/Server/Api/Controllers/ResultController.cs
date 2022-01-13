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
    [Route("api/[controller]")]
    public class ResultController : ApiController<Result>
    {
        public ResultController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet("{id}")]
        public Task<ActionResult<ApiResponse<Result>>> GetResult(int id)
        {
            return GetById(id);
        }

        [HttpGet]
        public Task<ActionResult<ApiResponse<IEnumerable<Result>>>> GetResults([FromQuery] string status)
        {
            return Get(status);
        }

        [HttpPost]
        public Task<ActionResult<ApiResponse<Result>>> CreateResult(ResultPostRequestBody request)
        {
            return Create<ResultPostRequestBody>(request);
        }

        [HttpPatch]
        public Task<ActionResult<ApiResponse<Result>>> UpdateResult(ResultPatchRequestBody request)
        {
           return Update<ResultPatchRequestBody>(request);
        }

        [HttpGet("summary")]
        public Task<ActionResult<ApiResponse<IEnumerable<ResultSummary>>>> GetSummary([FromQuery] string date)
        {
            return GetReportSummary(date);
        }
        

    }

}
