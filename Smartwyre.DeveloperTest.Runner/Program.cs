using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        IRebateDataStore rebateDataStore = new RebateDataStore();
        IProductDataStore productDataStore = new ProductDataStore();

        IRebateService rebateService = new RebateService(rebateDataStore, productDataStore);

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "testRebate",
            ProductIdentifier = "testProduct",
        };

        CalculateRebateResult result = rebateService.Calculate(request);

        if (result.Success)
        {
            Console.WriteLine("Rebate calculation successful!");
        }
        else
        {
            Console.WriteLine("Rebate calculation failed.");
        }
    }
}
