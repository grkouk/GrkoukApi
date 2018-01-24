using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrKouk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GrKouk.Api.Data
{
    public class ApiDbContext:DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options):base(options)
        {
            
        }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<RoomQuoteRequest> RoomQuoteRequests { get; set; }
    }
}
