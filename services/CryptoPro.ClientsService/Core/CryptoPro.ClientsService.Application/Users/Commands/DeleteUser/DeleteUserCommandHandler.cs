using AutoMapper;
using CryptoPro.ClientsService.Domain.Repositories;
using MediatR;

namespace CryptoPro.ClientsService.Application.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public DeleteUserCommandHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteUserAsync(request.Id);
    }
}