using AutoMapper;
using CryptoPro.ClientsService.Domain.Dtos;
using CryptoPro.ClientsService.Domain.Repositories;
using MediatR;

namespace CryptoPro.ClientsService.Application.Users.Queries.GetAllUsers;

public sealed class GetAllUsersQueryHandler: IRequestHandler<GetAllUsersQuery, IEnumerable<UserReadDto>>
{ 
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper; 
    }
    
    public async Task<IEnumerable<UserReadDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var allUsers = await _repository.GetAllUsersAsync();
        var mappedUsers = _mapper.Map<IEnumerable<UserReadDto>>(allUsers);
        
        return mappedUsers;
    }
}