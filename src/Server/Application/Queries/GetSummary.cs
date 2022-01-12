using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Aggregates;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries
{
    public class GetSummary : IRequest<IEnumerable<ResultSummary>> 
    {
         public GetSummary(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; }
    }

    public class GetSummaryHandler : IRequestHandler<GetSummary, IEnumerable<ResultSummary>> 
    {
        private readonly ISummaryRepository _summaryRepo;

        public GetSummaryHandler(ISummaryRepository summaryRepository)
        {
            _summaryRepo = summaryRepository;
        }

        public async Task<IEnumerable<ResultSummary>> Handle(GetSummary request, CancellationToken cancellationToken)
        {
            return await _summaryRepo.GetReportSummary(request.Date);
        }
    }
}