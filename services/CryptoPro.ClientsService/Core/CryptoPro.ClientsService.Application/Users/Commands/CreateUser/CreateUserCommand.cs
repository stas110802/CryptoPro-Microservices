using CryptoPro.ClientsService.Domain.Dtos;
using MediatR;

namespace CryptoPro.ClientsService.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(UserCreateDto User) : IRequest<bool>;
