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

        [ViewData]
        public CommandResult Confirmation { get; set; }

        public Index(IMediator mediator) => _mediator = mediator;

        public void OnGet() => Data = new Command();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Confirmation = new CommandResult
                {
                    IsSuccess = false,
                    ErrorMessage = "One or more fields require your attention.  Please complete the form and try again."
                };

                return Page();
            }

            Confirmation = await _mediator.Send(Data);
            return Page();
        }

        public class Command : IRequest<CommandResult>
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

        public class Handler : IRequestHandler<Command, CommandResult>
        {
            private readonly DonationContext _db;
            private readonly ILogger<Index> _logger;

            public Handler(DonationContext db, ILogger<Index> logger)
            {
                _db = db;
                _logger = logger;
            }

            public async Task<CommandResult> Handle(Command message, CancellationToken token)
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

                var result = new CommandResult();
                try
                {
                    var rowsAffected = await _db.SaveChangesAsync(token);
                    _logger.LogInformation("Donation successfully created Id = @0", donation.Id);
                    result.IsSuccess = (rowsAffected > 0);
                    if (result.IsSuccess)
                    {
                        result.ConfirmationNumber = donation.ConfirmationNumber;
                        result.ConfirmedFirstName = donation.FirstName;
                    }
                    return result;
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Unable to save donation");
                    return null;
                }
            }
        }

        public class CommandResult
        {
            public bool IsSuccess { get; set; }
            public string ErrorMessage { get; set; }
            public string ConfirmedFirstName { get; set; }
            public string ConfirmationNumber { get; set; }
        }
    }
}
