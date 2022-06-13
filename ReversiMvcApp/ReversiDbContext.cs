using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReversiMvcApp.Models;
using ReversiMvcApp.Models.Enums;
using System;
using System.Collections.Generic;

namespace ReversiMvcApp.Data
{
    public class ReversiDbContext : IdentityDbContext
    {
        public ReversiDbContext(DbContextOptions<ReversiDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Speler> Speler { get; set; }
        public DbSet<Spel> Spel { get; set; }
        public DbSet<Uitslag> Uitslag { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Spel>().Property(s => s.Bord).HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<Kleur[,]>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
            );
            builder.Entity<Spel>().Property(s => s.Beurten).HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<List<ValueTuple<int, int, int>>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
            );
        }
    }
}
