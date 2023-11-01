using MediatR;
using MyShop.Common.Exceptions;
using MyShop.Databases.Postgres.Repositories;
using MyShop.Domain.Models;

namespace MyShop.Application.Queries.Offers
{
    public record AllOffersQuery : IRequest<IEnumerable<Offer>>;

    public class AllOffersQueryHandler : IRequestHandler<AllOffersQuery, IEnumerable<Offer>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IPriceRepository _priceRepository;

        public AllOffersQueryHandler(
            IProductRepository productRepository,
            IStockRepository stockRepository,
            IPriceRepository priceRepository
            )
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _priceRepository = priceRepository;
        }
        public async Task<IEnumerable<Offer>> Handle(AllOffersQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var products = await _productRepository.GetProductsAsync();

            var productIds = products
                .Select(p => p.Id)
                .ToList();

            var prices = await _priceRepository.GetPricesByProductIdsAsync(productIds);

            var stocks = await _stockRepository.GetStocksByProductIdsAsync(productIds);

            var resultTemp = products
                .Join(
                    prices,
                    product => product.Id,
                    price => price.ProductId,
                    (product, price) =>
                    new Offer
                    {
                        ProductBrand = product.Brand,
                        ProductId = product.Id,
                        ProductName = product.Name,
                        ProductSize = product.Size,
                        Price = price.Amount
                    });
            var result = resultTemp
                .Join(
                    stocks,
                    productPrice => productPrice.ProductId,
                    stock => stock.ProductId,
                    (productPrice, stock) =>
                    new Offer
                    {
                        ProductBrand = productPrice.ProductBrand,
                        ProductId = productPrice.ProductId,
                        ProductName = productPrice.ProductName,
                        ProductSize = productPrice.ProductSize,
                        Price = productPrice.Price,
                        Quantity = stock.Amount
                    });

            return result;
        }
    }
}
