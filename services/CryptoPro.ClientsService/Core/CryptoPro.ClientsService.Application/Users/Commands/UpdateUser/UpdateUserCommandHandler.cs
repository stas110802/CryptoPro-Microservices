using AutoMapper;
using CryptoPro.ClientsService.Domain.Entities;
using CryptoPro.ClientsService.Domain.Repositories;
using MediatR;

namespace CryptoPro.ClientsService.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandHandler: IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        return await _repository.UpdateUserAsync(request.NewUser);
    }
}
