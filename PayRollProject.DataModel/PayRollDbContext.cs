using System.Diagnostics.CodeAnalysis;

namespace PayRollProject.DataModel
{
	using Common.PublicTools;
	using Entities.BaseClass;
	using Entities.Entities;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.ChangeTracking;
	using Microsoft.EntityFrameworkCore.Infrastructure;
	using Microsoft.EntityFrameworkCore.Metadata;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class PayRollDbContext : IdentityDbContext<ApplicationUsers, ApplicationRoles, string>
	{
		public PayRollDbContext(DbContextOptions<PayRollDbContext> options)
			: base(options)
		{
		}

		public DbSet<GeoProvinces> GeoProvinces { get; set; }

		public DbSet<GeoCounties> GeoCounties { get; set; }

		public DbSet<GeoDistricts> GeoDistricts { get; set; }

		public GeoNeighborhoods GeoNeighborhoods { get; set; }

		public DbSet<GeoRuralDistricts> GeoRuralDistricts { get; set; }

		public DbSet<AuditLog> AuditLogs { get; set; }

		//// public DbSet<Countries> Countries_Tbl { get; set; }
		public override int SaveChanges()
		{
			this.ApplyAuditing();
			return base.SaveChanges();
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			this.ApplyAuditing();
			return await base.SaveChangesAsync(cancellationToken);
		}

		

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			var assembly = typeof(IEntityObject).Assembly;
			builder.VerifyEntities<IEntityObject>(assembly);
			builder.Entity<AuditLog>().ToTable("AuditLogs");

			foreach (var relationship in builder.Model.GetEntityTypes()
				         .SelectMany((IMutableEntityType e) => e.GetForeignKeys())
				         .Where((IMutableForeignKey fk) => fk.PrincipalEntityType.ClrType == typeof(ApplicationUsers)))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}

			base.OnModelCreating(builder);
			builder.Entity<ApplicationUsers>((EntityTypeBuilder<ApplicationUsers> entity) =>
			{
				// تنظیمات AspNetUsers
				entity.ToTable(name: "UsersTbl");
				entity.Property((ApplicationUsers e) => e.Id).HasColumnName("UserId");

				// GUID
				entity.Property((ApplicationUsers e) => e.Id).ValueGeneratedOnAdd();
			});
			builder.Entity<ApplicationRoles>((EntityTypeBuilder<ApplicationRoles> entity) =>
			{
				// تنظیمات AspNetRoles
				entity.ToTable(name: "RolesTbl");
			});
		}

		private void ApplyAuditing()
		{
			var httpContextAccessor = this.GetService<IHttpContextAccessor>();
			var userId = httpContextAccessor?.HttpContext?.User
				?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

			var entries = ChangeTracker.Entries<FieldPublicInherits>()
				.Where((EntityEntry<FieldPublicInherits> e) => e.State == EntityState.Added);

			foreach (var entry in entries)
			{
				entry.Entity.UserID = userId ?? "system";
				entry.Entity.CreateDateTime = DateTime.Now;
			}
		}
	}
}