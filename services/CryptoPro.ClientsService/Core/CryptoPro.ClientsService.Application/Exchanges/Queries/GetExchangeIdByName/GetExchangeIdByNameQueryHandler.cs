using CryptoPro.ClientsService.Domain.Repositories;
using MediatR;

namespace CryptoPro.ClientsService.Application.Exchanges.Queries.GetExchangeIdByName;

public sealed class GetExchangeIdByNameQueryHandler : IRequestHandler<GetExchangeIdByNameQuery, int>
{
    private readonly IExchangeRepository _repository ;
    
    public GetExchangeIdByNameQueryHandler(IExchangeRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(GetExchangeIdByNameQuery request, CancellationToken cancellationToken)
    {
        var id = await _repository.GetExchangeIdByTypeAsync(request.Exchange);
        
        return id;
    }
}