using CryptoPro.BotsService.Domain.Entities;
using CryptoPro.BotsService.Domain.Repositories;
using MediatR;

namespace CryptoPro.BotsService.Application.Orders.Queries.GetUserSltpOrders;

public sealed class GetUserSltpOrdersQueryHandler : IRequestHandler<GetUserSltpOrdersQuery, IEnumerable<SltpOrderEntity>>
{
    private readonly ISltpOrderRepository _repository;

    public GetUserSltpOrdersQueryHandler(ISltpOrderRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<SltpOrderEntity>> Handle(GetUserSltpOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repository.GetUserOrders(request.UserId);
        
        return orders;
    }
}