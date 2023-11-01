using FluentValidation;
using Moq;
using MyShop.Application.Commands.Offers;
using MyShop.Databases.Postgres.Repositories;
using MyShop.Domain.Models;

namespace MyShop.Tests
{
    public class AddOffersCommandHandlerTests
    {
        [Fact]
        public async Task Success_AddOffer_ReturnsOffer()
        {
            // Arrange
            var initialOffer = new Offer
            {
                ProductBrand = "testbrand",
                ProductName = "testname",
                ProductSize = "testsize",
                Price = 19.99m,
                Quantity = 10
            };
            var expectedProduct = new Product { Id = 1, Brand = "testbrand", Name = "testname", Size = "testsize" };
            var expectedStock = new Stock { ProductId = 1, Amount = 10 };
            var expectedPrice = new Price { ProductId = 1, Amount = 19.99m };

            var mockProductRepository = new Mock<IProductRepository>();
            var mockStockRepository = new Mock<IStockRepository>();
            var mockPriceRepository = new Mock<IPriceRepository>();
            var mockOfferValidator = new Mock<IValidator<Offer>>();
            
            var command = new AddOffersCommand(initialOffer);
            var expectedResult = Offer.Build(new Product(), new Price(), new Stock());

            mockProductRepository
                .Setup(repository => repository.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync(expectedProduct);

            mockStockRepository
                .Setup(repository => repository.AddAsync(It.IsAny<Stock>()))
                .ReturnsAsync(expectedStock);

            mockPriceRepository
                .Setup(repository => repository.AddAsync(It.IsAny<Price>()))
                .ReturnsAsync(expectedPrice);

            // Act
            var handler = new AddOffersCommandHandler(
                mockProductRepository.Object,
                mockStockRepository.Object,
                mockPriceRepository.Object
            );
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.Id, result.ProductId);
            Assert.Equal(expectedProduct.Brand, result.ProductBrand);
            Assert.Equal(expectedProduct.Name, result.ProductName);
            Assert.Equal(expectedProduct.Size, result.ProductSize);
            Assert.Equal(expectedStock.Amount, result.Quantity);
            Assert.Equal(expectedPrice.Amount, result.Price);
        }
    }
}