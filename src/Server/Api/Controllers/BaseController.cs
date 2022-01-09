using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Extension;
using Application.Commands;
using Application.Queries;
using AutoMapper;
using Domain.Interfaces;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    public abstract class ApiController<T> : ControllerBase where T : IEntity
    {
        private readonly IMapper _mapper;
        public IMediator _mediator { get; }
        public ApiController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        protected async Task<ActionResult<ApiResponse<T>>> GetById(string testId)
        {
            try
            {
                var result = await _mediator.Send(new GetEntityById<T>(testId));
                if (result is not null)
                {
                    return Ok(ApiResponse<T>.FromData(result));
                }
                return NotFound(ApiResponse<T>.WithError(Responses.NotFound));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<T>
                .WithError(ex.Message));
            }
        }

        protected async Task<ActionResult<ApiResponse<IEnumerable<T>>>> Get(
            [FromQuery] string status = null, string email = null)
        {
            try
            {
                var result = await _mediator.Send(new GetEntity<T>(status, email));
                if (result is not null)
                {
                    return Ok(ApiResponse<IEnumerable<T>>.FromData(result));

                }
                return NotFound(ApiResponse<IEnumerable<T>>.WithError(Responses.NotFound));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<IEnumerable<T>>
                .WithError(ex.Message));
            }
        }

        public async Task<ActionResult<ApiResponse<S>>> GetSummary<S>(string filter = null) where S : class
        {
            try
            {
                var user = await _mediator.Send(new GetSummary<T, S>(filter));
                if (user is not null)
                {
                    return Ok(ApiResponse<S>.FromData(user));

                }
                return NotFound(ApiResponse<S>.WithError(Responses.NotFound));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<S>
                .WithError(ex.Message));

            }
        }

        protected async Task<ActionResult<ApiResponse<T>>> Create<S>(S request) where S : class
        {
            try
            {
                var createTest = _mapper.Map<SaveEntity<T>>(request);
                var result = await _mediator.Send(createTest);
                if (result is not null)
                {
                    return Ok(ApiResponse<T>.FromData(result));

                }
                return NotFound(ApiResponse<T>.WithError(Responses.NotFound));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<T>
                .WithError(ex.Message));
            }
        }

        protected async Task<ActionResult<ApiResponse<T>>> Update<S>(S request) where S : class
        {
            try
            {
                var createTest = _mapper.Map<UpdateEntity<T>>(request);
                var result = await _mediator.Send(createTest);
                if (result is not null)
                {
                    return Ok(ApiResponse<T>.FromData(result));

                }
                return NotFound(ApiResponse<T>.WithError(Responses.NotFound));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<T>
                .WithError(ex.Message));
            }
        }
    }
}