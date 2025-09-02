namespace PayRollProject.DataModel
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using PayRollProject.Entities.Entities;

    public class PayRollDbContext : IdentityDbContext<ApplicationUsers, ApplicationRoles, string>
    {
        public PayRollDbContext(DbContextOptions<PayRollDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUsers>((EntityTypeBuilder<ApplicationUsers> entity) =>
                {
                    // AspNetUsers
                    entity.ToTable(name: "UsersTbl");
                    entity.Property((ApplicationUsers e) => e.Id).HasColumnName("UserId");


                    // GUID
                    entity.Property((ApplicationUsers e) => e.Id).ValueGeneratedOnAdd();
                });
            builder.Entity<ApplicationRoles>((EntityTypeBuilder<ApplicationRoles> entity) =>
                {
                    // AspNetRoles
                    entity.ToTable(name: "RolesTbl");
                });
        }
    }
}