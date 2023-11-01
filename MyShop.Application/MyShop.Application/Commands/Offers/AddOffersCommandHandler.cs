using FluentValidation;
using MediatR;
using MyShop.Common.Exceptions;
using MyShop.Databases.Postgres.Repositories;
using MyShop.Domain.Mappers;
using MyShop.Domain.Models;

namespace MyShop.Application.Commands.Offers
{
    public record AddOffersCommand(
        Offer Offer
        ) : IRequest<Offer>;

    public class AddOffersCommandHandler : IRequestHandler<AddOffersCommand, Offer>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IPriceRepository _priceRepository;
        private readonly IValidator<Offer> _offerValidator;

        public AddOffersCommandHandler(
            IProductRepository productRepository,
            IStockRepository stockRepository,
            IPriceRepository priceRepository
            )
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _priceRepository = priceRepository;
        }

        public async Task<Offer> Handle(AddOffersCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();


            //TODO : Transactional add

            var product = await _productRepository.AddAsync(request.Offer.ToProduct());
            if (product is null)
            {
                throw new BusinessException("product has failed to add");
            }

            var stockTask = _stockRepository.AddAsync(request.Offer.ToStock(product.Id));
            var priceTask = _priceRepository.AddAsync(request.Offer.ToPrice(product.Id));

            await Task.WhenAll(stockTask, priceTask);

            var stock = await stockTask;
            var price = await priceTask;

            if (stock is null)
            {
                throw new BusinessException("stock has failed to add");
            }

            if (price is null)
            {
                throw new BusinessException("price has failed to add");
            }

            var result = Offer.Build(product, price, stock);

            return result;
        }
    }
}
