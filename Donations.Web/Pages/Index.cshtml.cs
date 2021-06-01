using Donations.Web.Data;
using Donations.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Donations.Web.Pages
{
    public class Index : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public Command Data { get; set; }

        public Index(IMediator mediator) => _mediator = mediator;

        public void OnGet() => Data = new Command();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _mediator.Send(Data);

            return RedirectToPage("./Index"); //SPA should show thank you not redirect
        }

        public class Command : IRequest<string>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public double DonationOption { get; set; }
            public double DonationAmount { get; set; }
            public bool IsCoveringCosts { get; set; }

            public string CardName { get; set; }
            public string CardNumber { get; set; }
            public string ExpiryDate { get; set; }
            public string CVC { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string StateProvince { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }

        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly DonationContext _db;
            private readonly ILogger<Index> _logger;

            public Handler(DonationContext db, ILogger<Index> logger)
            {
                _db = db;
                _logger = logger;
            }

            public async Task<string> Handle(Command message, CancellationToken token)
            {
                var donation = new Donation
                {
                    Email = message.Email,
                    FirstName = message.FirstName,
                    LastName = message.LastName,
                    Country = message.Country,
                    Address1 = message.Address1,
                    City = message.City,
                    StateProvince = message.StateProvince,
                    PostalCode = message.PostalCode,
                    DonationAmount = message.DonationAmount,
                    HasCoveredFees = message.IsCoveringCosts
                };

                if (!string.IsNullOrWhiteSpace(message.Address2))
                {
                    donation.Address2 = message.Address2;
                }

                //TODO replace with Covered Charges calculation
                donation.TotalDonation = message.DonationAmount;

                var payId = Guid.NewGuid(); // This would be replaced with an intent id from a payment processor
                donation.PaymentId = payId.ToString();
                donation.ConfirmationNumber = GuidEncoder.Encode(payId, 11);

                donation.DonatedOnUTC = DateTime.UtcNow;

                await _db.AddAsync(donation, token);

                bool isSuccess = false;
                try
                {
                    var result = await _db.SaveChangesAsync(token);
                    _logger.LogInformation("Donation successfully created Id = @0", donation.Id);
                    isSuccess = (result > 0);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Unable to save donation");
                    return string.Empty;
                }

                return isSuccess ? donation.ConfirmationNumber : string.Empty;
            }
        }
    }
}
