using System;

namespace Donations.Web.Services
{
    public class FeeCostCalculator
    {
        private readonly decimal _fixedFee;
        private readonly decimal _percentFee;

        public FeeCostCalculator(decimal fixedFee, decimal percentFee)
        {
            _fixedFee = fixedFee;
            _percentFee = percentFee;
        }

        public decimal CalculateFeeCosts(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("The amount must be greater any 0.00");

            var finalCharge = (amount + _fixedFee) / (1 - _percentFee); //Calculation based on how Stripe passes final fees onto the customers https://support.stripe.com/questions/passing-the-stripe-fee-on-to-customers
            var roundedCharge = Math.Round(finalCharge, 2, MidpointRounding.ToZero); //choose this rounding to pass the example test 5 donation with 0.30 fixed and 2.9% = $0.45 charged fee

            return roundedCharge - amount;
        }
    }
}
