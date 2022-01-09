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
    [Route("[controller]")]
    public class BookingController : ApiController<Booking>
    {

        public BookingController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
        
        [HttpGet]
        public Task<ActionResult<ApiResponse<IEnumerable<Booking>>>> GetBookings([FromQuery] string status)
        {
            return Get(status: status);
        }

        [HttpGet("{id}")]
        public Task<ActionResult<ApiResponse<Booking>>> GetBooking(string bookingId)
        {
            return GetById(bookingId);
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
