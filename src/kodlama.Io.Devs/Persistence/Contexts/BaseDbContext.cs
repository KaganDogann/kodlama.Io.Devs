using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext:DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<ProgrammingTechnology> ProgrammingTechnologies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public BaseDbContext(IConfiguration configuration, DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            Configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(entity =>
            {
                entity.ToTable("ProgrammingLanguages").HasKey(programmingLanguage => programmingLanguage.Id);
                entity.Property(programmingLanguage => programmingLanguage.Id).HasColumnName("Id");
                entity.Property(programmingLanguage => programmingLanguage.Name).HasColumnName("Name");

                
            });


            modelBuilder.Entity<ProgrammingTechnology>(a =>
            {
                a.ToTable("ProgrammingTechnologies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.Description).HasColumnName("Description");
                

                a.HasOne(c => c.ProgrammingLanguage);

            });

            ProgrammingTechnology[] programmingTechnologiesSeed = { new(1, "ASP.NET", "", 1), new(2, "WPF", "", 1), new(3, "Spring", "", 2), };
            modelBuilder.Entity<ProgrammingTechnology>().HasData(programmingTechnologiesSeed);

            ProgrammingLanguage[] programmingLanguagesSeed = { new(1, "C#"), new(2, "Java") };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguagesSeed);
        }
    }
}
