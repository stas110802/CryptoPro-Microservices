using CryptoPro.ExchangeClients.Domain.Clients.Models.CoinGecko;
using MediatR;

namespace CryptoPro.ExchangeClients.Application.Coins.Queries.GetDetailCoinInformationById;

public sealed record GetDetailCoinInformationByIdQuery(string Id)
    : IRequest<CoinFullDataById>;
