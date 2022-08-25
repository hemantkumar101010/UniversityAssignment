using Microsoft.EntityFrameworkCore;
using UniversityDataLibrary.Entities;

namespace UniversityDataLibrary
{
    public class UniversityDataDbContext:DbContext
    {
        public DbSet<University> Universities { get; set; }
        public UniversityDataDbContext()
        {

        }
        public UniversityDataDbContext(DbContextOptions options)
  : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<University>().HasIndex(u=>u.Name).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-QM194TV4\SQLEXPRESS;Database=UniversityEfDb;Trusted_Connection=True;");


        }

    }
}
