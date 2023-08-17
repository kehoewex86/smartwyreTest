using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Strategies
{
    public class FixedCashAmountIncentiveStrategy : IIncentiveStrategy
    {
        public bool IsRebateApplicable(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate != null &&
                   rebate.Incentive == IncentiveType.FixedCashAmount &&
                   product != null &&
                   rebate.Amount > 0 &&
                   product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount);
        }

        public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount;
        }
    }
}
