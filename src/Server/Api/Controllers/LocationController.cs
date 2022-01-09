using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using Api.Extension;
using Domain.Entities;
using Application.DTO;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LocationController : ApiController<Location>
    {

        public LocationController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
        
        public Task<ActionResult<ApiResponse<IEnumerable<Location>>>> GetLocations()
        {
            return Get();
        }

        [HttpPost]
        public Task<ActionResult<ApiResponse<Location>>> CreateLocation(LocationRequestBody request)
        {
            return Create<LocationRequestBody>(request);
        }

    }

}