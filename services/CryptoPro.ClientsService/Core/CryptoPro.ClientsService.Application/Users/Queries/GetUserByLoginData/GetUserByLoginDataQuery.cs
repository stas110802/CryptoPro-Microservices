using CryptoPro.ClientsService.Domain.Entities;
using MediatR;

namespace CryptoPro.ClientsService.Application.Users.Queries.GetUserByLoginData;

public sealed record GetUserByLoginDataQuery(string Login, string Password) : IRequest<UserEntity?>;