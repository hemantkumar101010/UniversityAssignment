using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityManagementWebApp.Areas.Identity.Data;
using static Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal.ExternalLoginModel;
using UniversityManagementWebApp.Models;

namespace UniversityManagementWebApp.Areas.Identity.Data;

public class UniversityDbContext : IdentityDbContext<UniversityAppUser>
{
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

       // builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }

    public DbSet<UniversityManagementWebApp.Models.University>? University { get; set; }
}

//internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<UniversityAppUser>
//{
//    public void Configure(EntityTypeBuilder<UniversityAppUser> builder)
//    {
//        builder.Property(u => u.PanNumber).HasMaxLength(10);
//    }
//}