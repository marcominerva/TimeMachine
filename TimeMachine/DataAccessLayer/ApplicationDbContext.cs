using Microsoft.EntityFrameworkCore;
using TimeMachine.DataAccessLayer.Entities;

namespace TimeMachine.DataAccessLayer;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Person> People { get; set; }

    public DbSet<PhoneNumber> PhoneNumbers { get; set; }

    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>(builder =>
        {
            builder.ToTable("People", tableBuilder =>
            {
                tableBuilder.IsTemporal(temporalBuilder =>
                {
                    temporalBuilder.HasPeriodStart("ValidFrom");
                    temporalBuilder.HasPeriodEnd("ValidTo");
                    temporalBuilder.UseHistoryTable("PeopleHistory");
                });
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
        });

        modelBuilder.Entity<PhoneNumber>(builder =>
        {
            builder.ToTable("PhoneNumbers", tableBuilder =>
            {
                tableBuilder.IsTemporal(temporalBuilder =>
                {
                    temporalBuilder.HasPeriodStart("ValidFrom");
                    temporalBuilder.HasPeriodEnd("ValidTo");
                    temporalBuilder.UseHistoryTable("PhoneNumbersHistory");
                });
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Type).HasConversion<string>().HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(x => x.Number).HasMaxLength(20).IsUnicode(false).IsRequired();

            builder.HasOne(x => x.Person).WithMany(x => x.PhoneNumbers).HasForeignKey(x => x.PersonId);
        });

        modelBuilder.Entity<City>(builder =>
        {
            builder.ToTable("Cities", tableBuilder =>
            {
                tableBuilder.IsTemporal(temporalBuilder =>
                {
                    temporalBuilder.HasPeriodStart("ValidFrom");
                    temporalBuilder.HasPeriodEnd("ValidTo");
                    temporalBuilder.UseHistoryTable("CitiesHistory");
                });
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(c => c.People).WithOne(p => p.City).HasForeignKey(nameof(Person.CityId));
        });
    }
}
