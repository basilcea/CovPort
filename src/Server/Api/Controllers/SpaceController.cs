using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;

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

        [HttpPost]
        public async Task<ActionResult<SpaceResponse>> CreateSpace(CreateSpaceCommand request)
        {
            
        }
        public async Task<ActionResult<IEnumerable<SpaceResponse>>> GetSpaces(CreateSpaceCommand request)
        {
        }
        [HttpPost("/locations")]
        public async Task<ActionResult<LocationResponse>> CreateLocation(string bookingId)
        {

        }

        [HttpGet("/locations")]
        public async Task<ActionResult<IEnumerable<LocationResponse>>> GetLocations()
        {

        }
    }

}
