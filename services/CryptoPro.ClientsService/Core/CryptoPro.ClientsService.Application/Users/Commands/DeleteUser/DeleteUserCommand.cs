using MediatR;

namespace CryptoPro.ClientsService.Application.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(int Id) : IRequest<bool>;
