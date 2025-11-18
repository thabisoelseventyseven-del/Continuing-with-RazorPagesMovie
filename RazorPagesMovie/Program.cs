using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

var builder = WebApplication.CreateBuilder(args);


// 1. Add Database + Identity

builder.Services.AddDbContext<RazorPagesMovieContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("RazorPagesMovieContext")
        ?? throw new InvalidOperationException("Connection string 'RazorPagesMovieContext' not found.")
    ));

// Add Identity (Login/Register/Logout)
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<RazorPagesMovieContext>();

// Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();


// 2. Seed Movie Data  

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}


// 3. Middleware (Order matters!)

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Identity needs these:
app.UseAuthentication();   // <-- required for login
app.UseAuthorization();    // <-- required for access control

app.UseRouting();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
