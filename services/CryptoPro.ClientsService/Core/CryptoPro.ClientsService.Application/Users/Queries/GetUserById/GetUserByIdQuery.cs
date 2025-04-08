using CryptoPro.ClientsService.Domain.Dtos;
using MediatR;

namespace CryptoPro.ClientsService.Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(int Id) : IRequest<UserReadDto?>;