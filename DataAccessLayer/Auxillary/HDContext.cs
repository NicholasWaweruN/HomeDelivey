
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DataAccessLayer.ViewModels.HD_ViewModels;

namespace DataAccesLayer.Models.Auxillary
{
    public class HDContext : IdentityDbContext<IdentityUser>
    {
        public HDContext(DbContextOptions<HDContext> options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalog>().HasNoKey();
            modelBuilder.Entity<PreviousOrdersViewModel>().HasNoKey();
            modelBuilder.Entity<CustomerDetails>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }
    }
}