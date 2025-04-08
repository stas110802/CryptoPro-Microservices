using CryptoPro.ClientsService.Domain.Entities;
using CryptoPro.ClientsService.Domain.Repositories;
using MediatR;

namespace CryptoPro.ClientsService.Application.Users.Queries.GetUserByLoginData;

public sealed class GetUserByLoginDataQueryHandler : IRequestHandler<GetUserByLoginDataQuery, UserEntity?>
{
    private readonly IUserRepository _repository;
    
    public GetUserByLoginDataQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<UserEntity?> Handle(GetUserByLoginDataQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByLoginData(request.Login, request.Password);
        
        return user;
    }
}