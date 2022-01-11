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
    public class ApiController<T> : ControllerBase where T : IEntity
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
            [FromQuery] string filter = null)
        {
            try
            {
                var result = await _mediator.Send(new GetEntity<T>(filter));
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

        public async Task<ActionResult<ApiResponse<IEnumerable<S>>>> GetSummary<S>(string filter = null) where S : class
        {
            try
            {
                var user = await _mediator.Send(new GetSummary<S,T>(filter));
                if (user is not null)
                {
                    return Ok(ApiResponse<IEnumerable<S>>.FromData(user));

                }
                return NotFound(ApiResponse<IEnumerable<S>>.WithError(Responses.NotFound));

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
                var createTest = _mapper.Map<SaveEntity<S,T>>(request);
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
                var createTest = _mapper.Map<UpdateEntity<S,T>>(request);
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