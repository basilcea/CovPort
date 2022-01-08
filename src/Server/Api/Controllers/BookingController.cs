using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using AutoMapper;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ActionResult<IEnumerable<BookingResponse>>> GetBookings()
        {
         
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingResponse>> GetBooking(string bookingId)
        {

        }

        [HttpPost]
        public async Task<ActionResult<BookingResponse>> CreateBooking(CreateBookingCommand request)
        {
        }

        [HttpPatch]
        public async Task<ActionResult<BookingResponse>> CancelBooking(CancelBookingCommand request)
        {
        }


    }

}
