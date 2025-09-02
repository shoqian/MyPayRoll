using PayRollProject.DataModel.Services.Interface;
using PayRollProject.DataModel.Services.Repository;
using PayRollProject.WebFrameWork.Extensions;

var builder = WebApplication.CreateBuilder(args);

var appSettingConfig = builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(FileNameExtensions.AppSettingName).Build();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DataBase Services
builder.Services.AddDbContextService(appSettingConfig);

// Add Identity Services
builder.Services.AddIdentityService();

#region Add Services


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



#endregion




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(name: "default", pattern: "{controller=Account}/{action=Login}/{id?}").WithStaticAssets();

app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "AdminArea",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    });

app.Run();