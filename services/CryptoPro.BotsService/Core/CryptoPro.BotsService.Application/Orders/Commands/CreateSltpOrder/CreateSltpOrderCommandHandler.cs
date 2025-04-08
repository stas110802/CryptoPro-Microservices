using CryptoPro.BotsService.Domain.Entities;
using CryptoPro.BotsService.Domain.Repositories;
using MediatR;

namespace CryptoPro.BotsService.Application.Orders.Commands.CreateSltpOrder;

public sealed class CreateSltpOrderCommandHandler : IRequestHandler<CreateSltpOrderCommand, bool>
{
    private readonly ISltpOrderRepository _repository;

    public CreateSltpOrderCommandHandler(ISltpOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(CreateSltpOrderCommand request, CancellationToken cancellationToken)
    {
        var order = request.Order;
        var exchangeId = 0; //todo ЗАМЕНИТЬ НА ЗАПРОС в CLIENTS SERVICE 
        var entity = new SltpOrderEntity
        {
            Currency = order.Currency,
            UpperPrice = order.UpperPrice,
            BottomPrice = order.BottomPrice,
            SellPrice = order.SellPrice,
            Amount = order.Amount,
            Type = order.Type,
            ExchangeId = exchangeId,
            UserId = order.UserId
        };
        var isSuccessful = await _repository.AddOrder(entity);

        return isSuccessful;
    }
}