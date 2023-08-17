using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Strategies
{
    public interface IIncentiveStrategy
    {
        bool IsRebateApplicable(Rebate rebate, Product product, CalculateRebateRequest request);
        decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}
