using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using Api.Extension;
using Domain.Entities;
using Application.DTO;
using System;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpaceController : ApiController<Space>
    {

        public SpaceController(IMediator mediator,  IMapper mapper): base(mediator, mapper)
        {
        }

  [HttpGet]
        public Task<ActionResult<ApiResponse<IEnumerable<Space>>>> GetSpaces()
        {
            return Get();
        }

        [HttpPost]
        public Task<ActionResult<ApiResponse<Space>>> CreateSpace(SpaceRequestBody request)
        {
           return Create<SpaceRequestBody>(request);
        }

    }

}
