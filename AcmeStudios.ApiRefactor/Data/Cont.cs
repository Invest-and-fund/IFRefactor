using AcmeStudios.ApiRefactor.Models;
using Microsoft.EntityFrameworkCore;

namespace AcemStudios.ApiRefactor.Data
{
    public class Cont : DbContext
    {
        public DbSet<PaymentCharge> PaymentCharges { get; set; }
        public Cont(DbContextOptions<Cont> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
