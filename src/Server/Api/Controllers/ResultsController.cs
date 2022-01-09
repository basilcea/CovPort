using System;
using System.Threading.Tasks;
using Api.Response;
using Application.Queries;
using AutoMapper;
using Domain.Aggregates;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ResultsController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Result>>> GetResult(string testId)
        {

            try
            {
                var result = await _mediator.Send(new GetEntityById<Result>(testId));
                if (result is not null)
                {
                    return Ok(ApiResponse<Result>.FromData(result));

                }
                return NotFound(ApiResponse<Result>.WithError(Responses.NotFound));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<Result>.WithError(ex.Message));

            }
        }


[HttpGet]
        public async Task<ActionResult<ApiResponse<Result>>> GetResults([FromQuery] string status)
        {

            try
            {
                var result = await _mediator.Send(new GetEntityByFilters<Result>(status));
                if (result is not null)
                {
                    return Ok(ApiResponse<Result>.FromData(result));

                }
                return NotFound(ApiResponse<Result>.WithError(Responses.NotFound));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<Result>.WithError(ex.Message));

            }
        }

        // [HttpPost]
        // public async Task<ActionResult<ApiResponse<Result>>> CreateResult(string testId)
        // {

        //     try
        //     {
        //         var result = await _mediator.Send(new GetEntityById<Result>(testId));
        //         if (result is not null)
        //         {
        //             return Ok(ApiResponse<Result>.FromData(result));

        //         }
        //         return NotFound(ApiResponse<Result>.WithError(Responses.NotFound));

        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<Result>.WithError(ex.Message));

        //     }
        // }

        // [HttpPatch]
        // public async Task<ActionResult<ApiResponse<Result>>> UpdateResult(string testId)
        // {

        //     try
        //     {
        //         var result = await _mediator.Send(new GetEntityById<Result>(testId));
        //         if (result is not null)
        //         {
        //             return Ok(ApiResponse<Result>.FromData(result));

        //         }
        //         return NotFound(ApiResponse<Result>.WithError(Responses.NotFound));

        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<Result>.WithError(ex.Message));

        //     }
        // }



        [HttpGet("/summary")]
        public async Task<ActionResult<ApiResponse<ResultSummary>>> GetSummary()
        {
             try
            {
                var result = await _mediator.Send(new GetResultSummary());
                if (result is not null)
                {
                    return Ok(ApiResponse<ResultSummary>.FromData(result));

                }
                return NotFound(ApiResponse<ResultSummary>.WithError(Responses.NotFound));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<ResultSummary>.WithError(ex.Message));

            }
        }



    }

}
