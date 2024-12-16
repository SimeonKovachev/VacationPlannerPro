using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VacationPlannerPro.Data;
using VacationPlannerPro.Business.Config;
using VacationPlannerPro.Data.Seeders;
using VacationPlannerPro.Web.Middlewares;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

// DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
     .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddServiceLayer(builder.Configuration);

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    context.Database.Migrate();

    //Seed data
    await ProfessionSeeder.SeedProfessionsAsync(context);
    await RoleAndUserSeeder.SeedRolesAndUsersAsync(roleManager, userManager);
    await InitialDataSeeder.SeedInitialData(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Admin" })
    .RequireAuthorization(new AuthorizeAttribute { Roles = "Administrator" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseExceptionHandler("/Error/Handle");
app.UseStatusCodePagesWithReExecute("/Error/StatusCode", "?code={0}");
app.UseGlobalErrorHandlerMiddleware();

app.MapRazorPages();

app.Run();
