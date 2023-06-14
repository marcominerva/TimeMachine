using Microsoft.EntityFrameworkCore;
using TimeMachine.DataAccessLayer.Entities;

namespace TimeMachine.DataAccessLayer;

public class ApplicationDbContext : DbContext
{
    public DbSet<Person> People { get; set; }

    public DbSet<PhoneNumber> PhoneNumbers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>(builder =>
        {
            builder.ToTable("People");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.City).HasMaxLength(50);
        });

        modelBuilder.Entity<PhoneNumber>(builder =>
        {
            builder.ToTable("PhoneNumbers");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Type).HasConversion<string>().HasMaxLength(20).IsUnicode(false).IsRequired();
            builder.Property(x => x.Number).HasMaxLength(20).IsUnicode(false).IsRequired();

            builder.HasOne(x => x.Person).WithMany(x => x.PhoneNumbers).HasForeignKey(x => x.PersonId);
        });
    }
}
