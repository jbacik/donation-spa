using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Donations.Web.Pages
{
    public class Index : PageModel
    {
        private readonly ILogger<Index> _logger;

        [BindProperty]
        public Command Data { get; set; }

        public Index(ILogger<Index> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            return RedirectToPage("./Index"); //SPA should show thank you not redirect
        }

        public class Command
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
    }
}
