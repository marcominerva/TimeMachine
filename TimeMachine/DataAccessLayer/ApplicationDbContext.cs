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

    protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
}
