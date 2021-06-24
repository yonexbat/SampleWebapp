using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleWebApp.Models.Entities;

namespace SampleWebApp.Data
{
    public class SampleWebAppContext : DbContext
    {
        public SampleWebAppContext (DbContextOptions<SampleWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<SampleWebApp.Models.Entities.GuestBookEntry> GuestBookEntry { get; set; }
    }
}
