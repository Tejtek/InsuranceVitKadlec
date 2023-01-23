using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InsuranceVitKadlec.Models;
using System.Configuration;

namespace InsuranceVitKadlec.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<InsuranceVitKadlec.Models.Insured> Insured { get; set; }
        public DbSet<InsuranceVitKadlec.Models.Insurence> Insurence { get; set; }
        public DbSet<InsuranceVitKadlec.Models.InsuredesInsurences> InsuredesInsurences { get; set; }
        public DbSet<InsuranceVitKadlec.Models.InsuredInsurenceEvent> InsuredInsurenceEvent { get; set; }
        public DbSet<InsuranceVitKadlec.Models.InsuredesInsurencesHistory> InsuredesInsurencesHistory { get; set; }
        public DbSet<InsuranceVitKadlec.Models.InsuredHistory> InsuredHistory { get; set; }
        public DbSet<InsuranceVitKadlec.Models.Article> Article { get; set; }

    }
}