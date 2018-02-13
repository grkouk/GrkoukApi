using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrKouk.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GrKouk.Api.Data
{
    public class ApiDbContext:DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options):base(options)
        {
            
        }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<RoomQuoteRequest> RoomQuoteRequests { get; set; }
        public DbSet<VehicleQuoteRequest> VehicleQuoteRequests { get; set; }
        //Προσθήκη 11/02/2018
        public DbSet<Company> Companies { get; set; }
        public DbSet<Transactor> Transactors { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region "Cascading Deletes prevention"

            modelBuilder.Entity<Transaction>()
                .HasOne(en => en.Transactor)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}
