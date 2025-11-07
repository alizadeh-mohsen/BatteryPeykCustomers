using BatteryPeykCustomers.Data;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure; // <--- add this
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Set QuestPDF license once at startup
QuestPDF.Settings.License = LicenseType.Community;

CultureInfo.DefaultThreadCurrentCulture
  = CultureInfo.DefaultThreadCurrentUICulture
  = PersianDateExtensionMethods.GetPersianCulture();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddCors();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("defaultConnection")));
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = Text.Plain;
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error != null)
                await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.Message);
            if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error != null)
                await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.ToString());
        });
    });
    app.UseHsts();
}
app.UseStaticFiles();

app.UseRouting();
app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowAnyOrigin()); // allow credentials

//SeedDatabase();
app.UseAuthorization();



app.MapRazorPages();
app.MapControllers();
app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}

//MASTER 1
//MASTER 2