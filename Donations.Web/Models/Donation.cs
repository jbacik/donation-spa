using System;
using System.ComponentModel.DataAnnotations;

namespace Donations.Web.Models
{
    public class Donation
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(254)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Country { get; set; }
        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }
        [StringLength(100)]
        public string Address2 { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }
        [Required]
        [StringLength(50)]
        public string StateProvince { get; set; }
        [Required]
        [StringLength(10)]
        public string PostalCode { get; set; }
        [Required]
        [StringLength(50)]
        public string PaymentId { get; set; }
        [Required]
        public double DonationAmount { get; set; }
        public string ConfirmationNumber { get; set; }
        public bool HasCoveredFees { get; set; }
        public double CoveredFeeAmount { get; set; }
        public double TotalDonation { get; set; }
        public DateTime DonatedOnUTC { get; set; }
    }
}
