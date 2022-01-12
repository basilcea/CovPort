using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using Domain.Entities;
using Api.Extension;
using Application.DTO;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ApiController<Booking>
    {

        public BookingController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
        
        [HttpGet]
        public Task<ActionResult<ApiResponse<IEnumerable<Booking>>>> GetBookings([FromQuery] string filter)
        {
            return Get(filter);
        }

        [HttpGet("{id}")]
        public Task<ActionResult<ApiResponse<Booking>>> GetBooking(int id)
        {
            return GetById(id);
        }

        [HttpPost]
        public Task<ActionResult<ApiResponse<Booking>>> CreateBooking(BookingPostRequestBody request)
        { 
            return Create<BookingPostRequestBody>(request);
        }


        [HttpPatch]
      
        public Task<ActionResult<ApiResponse<Booking>>> CancelBooking(BookingPatchRequestBody request)
        { 
            return Update<BookingPatchRequestBody>(request);
        }

    }

}
