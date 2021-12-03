using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XInsuranceCompany.Core.Entities.Insurance;
using XInsuranceCompany.Data.Mappings;

namespace XInsuranceCompany.Data.Contexts
{
    public class XInsuranceCompanyDbContext : DbContext
    {

        public XInsuranceCompanyDbContext(DbContextOptions<XInsuranceCompanyDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=OMERA-PC;Database=XInsuranceCompany; User Id = sa; Password = Sql2014r2!");

        //    //optionsBuilder.UseSqlServer(@"Server=.; Database=XInsuranceCompany; Trusted_Connection=true;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarInsuranceOfferMapping());
            modelBuilder.ApplyConfiguration(new CarInsuranceOfferDetailMapping());

            modelBuilder.Entity<CarInsuranceOfferDetail>()
                .HasOne<CarInsuranceOffer>(x => x.CarInsuranceOffer)
                .WithMany(x => x.CarInsuranceOfferDetails)
                .HasForeignKey(x=>x.CarInsuranceOfferId);
        }

        public DbSet<CarInsuranceOffer> CarInsuranceOffers { get; set; }
        public DbSet<CarInsuranceOfferDetail> CarInsuranceOfferDetails { get; set; }
    }
}
