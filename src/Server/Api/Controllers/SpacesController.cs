using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Api.Response;
using Domain.Entities;
using Application.Queries;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpaceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SpaceController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActionResult<ApiResponse<IEnumerable<Space>>>> GetSpaces()
        {
            try
            {
                var result = await _mediator.Send(new GetEntity<Space>());
                if (result is not null)
                {
                    return Ok(ApiResponse<Space>.FromData(result));

                }
                return NotFound(ApiResponse<Space>.WithError(Responses.NotFound));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<Space>.WithError(ex.Message));

            }
        }



        // [HttpPost]
        // public async Task<ActionResult<SpaceResponse>> CreateSpace(CreateSpaceCommand request)
        // {

        // }


        [HttpGet("/locations")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Location>>>> GetLocations()
        {
            try
            {
                var result = await _mediator.Send(new GetEntity<Location>());
                if (result is not null)
                {
                    return Ok(ApiResponse<Location>.FromData(result));

                }
                return NotFound(ApiResponse<Location>.WithError(Responses.NotFound));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<Location>.WithError(ex.Message));

            }
        }


        // [HttpPost("/locations")]
        // public async Task<ActionResult<LocationResponse>> CreateLocation(string bookingId)
        // {

        // }


    }

}
