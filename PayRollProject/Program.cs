using Microsoft.Extensions.Logging.Console;
using PayRollProject.DataModel;
using PayRollProject.DataModel.Seeder;
using PayRollProject.Entities.Entities;
using Syncfusion.Licensing;

var builder = WebApplication.CreateBuilder(args);

var appSettingConfig = builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile(FileNameExtensions.AppSettingName).Build();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DataBase Services
builder.Services.AddDbContextService(appSettingConfig);

// Add Identity Services
builder.Services.AddIdentityService();

#region Add Services

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBaseTableRepository, BaseTableRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuditService, AuditService>();

#endregion

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

#region Seeder NationalCodePrefixes

if (app.Environment.IsDevelopment())
{
	using (var scop = app.Services.CreateScope())
	{
		var db = scop.ServiceProvider.GetRequiredService<PayRollDbContext>();
		var seeder = new NationalCodeSeeder(db);
		var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Data/", "NationalCodePrefixes.json");

		if (!db.Set<Provinces>().Any())
		{
			seeder.SeedFromJson(jsonFilePath);
		}
	}
}

#endregion

#region Seeder Geo

if (app.Environment.IsDevelopment())
{
	using (var scop = app.Services.CreateScope())
	{
		var uow = scop.ServiceProvider.GetRequiredService<IUnitOfWork>();
		var audit = scop.ServiceProvider.GetRequiredService<IAuditService>();


		var seeder = new GeoSeeder(uow, audit);
		var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Data/", "listEn.json");

		if (!File.Exists(jsonFilePath))
		{
			// اگر فایل وجود ندارد، هشدار بدهید و از ادامه فرآیند کاشت داده صرف‌نظر کنید.
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"[GeoSeeder] Geo data JSON file not found.{jsonFilePath} Skipping geo data seeding.");
			Console.ResetColor();
		}
		else
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("[GeoSeeder] Starting geo data seeding...");
			Console.ResetColor();
			try
			{
				await seeder.SeedFromJsonAsync(jsonFilePath);

				Console.ForegroundColor = ConsoleColor.DarkGreen;
				Console.WriteLine("[GeoSeeder] Geo data seeding completed successfully.");
				Console.ResetColor();
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine($"[GeoSeeder] An error occurred during geo data seeding: {e.Message}");
				Console.ResetColor();
			}
		}
	}
}
else
{
	return;
}

#endregion

#region SyncFusion Service

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
	"@32392e302e303b32393bKq35AiUSRDJT5uIaFzRCrJWDo7gKUKH1Rwb6jH+WX4o=");

app.UseRequestLocalization("fa");

#endregion


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();


// این خط نگه می‌دارد Area منطقی "AdminArea" ولی URL ها با "AdminPanel" تولید/مسیر‌دهی می‌شوند
app.MapAreaControllerRoute(
	name: "AdminPanelRoute",
	areaName: "AdminArea",
	pattern: "AdminPanel/{controller=Home}/{action=Index}/{id?}"
);

// روت پیش‌فرض بقیه controllers
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Account}/{action=Login}/{id?}"
).WithStaticAssets();

app.Run();