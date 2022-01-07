namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet("/test/{id}")]
        public async Task<ActionResult<BookingResponse>> GetTestReport(string TestId)
        {

        }

        [HttpGet("/summary")]
        public async Task<ActionResult<BookingResponse>> GetSummary(BookingRequest request)
        {
        }



    }

}
