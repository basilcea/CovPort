using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Extension;
using Application.Commands;
using Application.Queries;
using AutoMapper;
using Domain.Aggregates;
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

        protected async Task<ActionResult<ApiResponse<T>>> GetById(int testId)
        {
            var result = await _mediator.Send(new GetEntityById<T>(testId));
            return Ok(ApiResponse<T>.FromData(result, GetMessage("GetById")));
        }

        protected async Task<ActionResult<ApiResponse<IEnumerable<T>>>> Get(
            [FromQuery] string filter = null)
        {
            var result = await _mediator.Send(new GetEntity<T>(filter));
            return Ok(ApiResponse<IEnumerable<T>>.FromData(result, GetMessage("Get")));
        }

        protected async Task<ActionResult<ApiResponse<IEnumerable<ResultSummary>>>> GetReportSummary([FromQuery] string date)
        {
            var dateString = date ?? DateTime.Now.ToShortDateString();
            var result = await _mediator.Send(new GetSummary(DateTime.Parse(dateString)));
            return Ok(ApiResponse<IEnumerable<ResultSummary>>.FromData(result, GetMessage("Summary")));
        }

        protected async Task<ActionResult<ApiResponse<T>>> Create<S>(S request) where S : class
        {
            var createTest = _mapper.Map<SaveEntity<T>>(request);
            var result = await _mediator.Send(createTest);
            return Ok(ApiResponse<T>.FromData(result, GetMessage("Create")));
        }

        protected async Task<ActionResult<ApiResponse<T>>> Update<S>(S request) where S : class
        {
            var createTest = _mapper.Map<UpdateEntity<T>>(request);
            var result = await _mediator.Send(createTest);
            return Ok(ApiResponse<T>.FromData(result, GetMessage("Update")));


        }

        protected static string GetMessage(string requestType)
        {
            return $"{typeof(T).Name} {requestType} Request Successful";

        }
        protected static string GetFailedMessage(string requestType)
        {
            return $"{typeof(T).Name} {requestType} Request Failed";

        }
    }
}