using Donations.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Donations.Web.Data
{
    public class DonationContext : DbContext
    {
        public DonationContext(DbContextOptions<DonationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donation>().ToTable("Donation");
        }
    }
}
