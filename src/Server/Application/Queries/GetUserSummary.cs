using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Aggregates;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries
{
    public class GetUserSummary : IRequest<UserSummary> 
    {
         public GetUserSummary(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class GetUserSummaryHandler : IRequestHandler<GetUserSummary, UserSummary> 
    {
        private readonly ISummaryRepository _summaryRepo;

        public GetUserSummaryHandler(ISummaryRepository summaryRepository)
        {
            _summaryRepo = summaryRepository;
        }

        public async Task<UserSummary> Handle(GetUserSummary request, CancellationToken cancellationToken)
        {
            return await _summaryRepo.GetUserSummary(request.Id);
        }
    }
}