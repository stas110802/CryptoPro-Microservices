using CryptoPro.ClientsService.Domain.Dtos;
using MediatR;

namespace CryptoPro.ClientsService.Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(UserUpdateDto NewUser) : IRequest<bool>;
