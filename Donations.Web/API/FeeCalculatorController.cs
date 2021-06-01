using Donations.Web.Data;
using Donations.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Donations.Web.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeCalculatorController : ControllerBase
    {
        private readonly PaymentProcessorSettings _paymentProcessorSettings;

        public FeeCalculatorController(IOptions<PaymentProcessorSettings> opts)
        {
            _paymentProcessorSettings = opts.Value;
        }

        [HttpGet]
        public decimal Get(decimal amount)
        {
            var calculator = new FeeCostCalculator(_paymentProcessorSettings.FixedFee, _paymentProcessorSettings.PercentFee);
            return calculator.CalculateFeeCosts(amount);
        }
    }
}
