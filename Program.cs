using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using StudentAttendanceManagementSystem.Data;
using StudentAttendanceManagementSystem.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
//     options.AddPolicy("SupervisorPolicy", policy => policy.RequireRole("Supervisor"));
//     options.AddPolicy("StudentPolicy", policy => policy.RequireRole("Student"));
// });

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
});

// Seed roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using(var scope = app.Services.CreateScope())
{
    var roleManager = 
     scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
       var roles = new[] { "Admin", "Supervisor", "Student" };

       foreach(var role in roles)
       {
        if(!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
       }
}

using(var scope = app.Services.CreateScope())
{
    var userManager = 
     scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    
    string email = "joel.admin@gmail.com";
    string password = "Admin@1234";
    string FirstName = "Joel";
    string LastName = "Admin";
    string phone = "0874040490";
    string address = "Dublin, Ireland";
    if (await userManager.FindByEmailAsync(email) == null){
        var user = new User
        {
            Email = email,
            UserName = email,
            PhoneNumber = phone,
            Address = address,
            FirstName = FirstName,
            LastName = LastName

        };

        var result = await userManager.CreateAsync(user, password);
        await  userManager.AddToRoleAsync(user, "Admin");
          
       }
}

app.Run();

