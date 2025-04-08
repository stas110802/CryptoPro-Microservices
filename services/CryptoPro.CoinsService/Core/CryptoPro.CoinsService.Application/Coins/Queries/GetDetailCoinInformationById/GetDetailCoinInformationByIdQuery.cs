using CryptoPro.CoinsService.Domain.Models.CoinGecko;
using MediatR;

namespace CryptoPro.CoinsService.Application.Coins.Queries.GetDetailCoinInformationById;

public sealed record GetDetailCoinInformationByIdQuery(string Id)
    : IRequest<CoinDetailedInformation>;
