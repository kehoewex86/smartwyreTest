using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    [Fact]
    public void Calculate_WithApplicableFixedCashAmountStrategy_ReturnsSuccessfulResult()
    {
        // Arrange
        var request = new CalculateRebateRequest();
        var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 100 };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

        var rebateDataStoreMock = new Mock<IRebateDataStore>();
        rebateDataStoreMock.Setup(rds => rds.GetRebate(It.IsAny<string>())).Returns(rebate);

        var productDataStoreMock = new Mock<IProductDataStore>();
        productDataStoreMock.Setup(pds => pds.GetProduct(It.IsAny<string>())).Returns(product);

        var rebateService = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object);

        // Act
        var result = rebateService.Calculate(request);

        // Assert
        Assert.True(result.Success);
        rebateDataStoreMock.Verify(rds => rds.StoreCalculationResult(rebate, rebate.Amount), Times.Once);
    }

    [Fact]
    public void Calculate_WithoutApplicableStrategy_ReturnsUnsuccessfulResult()
    {
        // Arrange
        var request = new CalculateRebateRequest();

        var rebateDataStoreMock = new Mock<IRebateDataStore>();
        var productDataStoreMock = new Mock<IProductDataStore>();

        var rebateService = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object);

        // Act
        var result = rebateService.Calculate(request);

        // Assert
        Assert.False(result.Success);
        rebateDataStoreMock.Verify(rds => rds.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Never);
    }
}
