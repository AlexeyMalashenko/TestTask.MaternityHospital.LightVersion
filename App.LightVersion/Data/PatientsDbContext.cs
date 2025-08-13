using App.LightVersion.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace App.LightVersion.Data
{
    public class PatientsDbContext : DbContext
    {
        public PatientsDbContext(DbContextOptions<PatientsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients => Set<Patient>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(builder =>
            {
                builder.HasKey(p => p.Id);

                builder.OwnsOne(p => p.Name, nameBuilder =>
                {
                    nameBuilder.Property(n => n.Family).IsRequired();
                    nameBuilder.Property(n => n.Use);

                    // ValueConverter для Given
                    nameBuilder.Property(n => n.Given)
                        .HasConversion(
                            v => string.Join(',', v),                         // List<string> -> string
                            v => v.Split(',', StringSplitOptions.None).ToList() // string -> List<string>
                        )
                        .Metadata.SetValueComparer(
                            new ValueComparer<List<string>>(
                                (c1, c2) => c1.SequenceEqual(c2),           // сравнение
                                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), // хэш
                                c => c.ToList()                               // клонирование
                            )
                        );
                });

                builder.Property(p => p.BirthDate).IsRequired();
                builder.Property(p => p.Gender).IsRequired();
                builder.Property(p => p.IsActive).IsRequired();
            });
        }
    }
}
