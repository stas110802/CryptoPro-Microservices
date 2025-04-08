using CryptoPro.ClientsService.Domain.Dtos;
using MediatR;

namespace CryptoPro.ClientsService.Application.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery : IRequest<IEnumerable<UserReadDto>>;