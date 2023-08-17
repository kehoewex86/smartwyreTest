using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Strategies;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Services
{
    public class RebateService : IRebateService
    {
        private readonly IRebateDataStore _rebateDataStore;
        private readonly IProductDataStore _productDataStore;

        public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore)
        {
            _rebateDataStore = rebateDataStore;
            _productDataStore = productDataStore;
        }

        public CalculateRebateResult Calculate(CalculateRebateRequest request)
        {
            var result = new CalculateRebateResult();
            var rebateAmount = 0m;

            var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
            var product = _productDataStore.GetProduct(request.ProductIdentifier);

            var incentiveStrategies = new List<IIncentiveStrategy>
            {
                new FixedCashAmountIncentiveStrategy(),
            };

            foreach (var strategy in incentiveStrategies)
            {
                if (strategy.IsRebateApplicable(rebate, product, request))
                {
                    rebateAmount = strategy.CalculateRebateAmount(rebate, product, request);
                    result.Success = true;
                    break;
                }
            }

            if (result.Success)
            {
                _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
            }

            return result;
        }
    }
}