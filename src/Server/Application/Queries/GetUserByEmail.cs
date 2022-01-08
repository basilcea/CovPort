using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries
{
    public class GetUserByEmail : IRequest<User>
    {
        public GetUserByEmail(string email){
            Email = email;
        }

        public string Email {get;set;}
    }

    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmail, User>
    {
        private readonly IUserRepository _userRepo;

        public GetUserByEmailHandler(IUserRepository userRepo){
            _userRepo = userRepo;
        }
        public async Task<User> Handle(GetUserByEmail request, CancellationToken cancellationToken)
        {
            return await _userRepo.GetUserByEmail(request.Email, cancellationToken);
        }
    }



}