using Gitea_1.GiteaMain.Data;
using Gitea_1.GiteaMain.Models;
using Gitea_1.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------
// 1️⃣ CONFIGURATION
// ---------------------------------------------------------

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();


// Load configuration (from appsettings.json)
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// ---------------------------------------------------------
// 2️⃣ DATABASE CONTEXT (SQLite)
// ---------------------------------------------------------

builder.Services.AddDbContext<GiteaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ---------------------------------------------------------
// 3️⃣ DEPENDENCY INJECTION - Register Services
// ---------------------------------------------------------

builder.Services.AddScoped<IInitService, InitService>();// change made by aarushi 
builder.Services.AddScoped<IObjectService, ObjectService>();
builder.Services.AddScoped<IFileService, FileService>();

// ---------------------------------------------------------
// 4️⃣ BUILD APP
// ---------------------------------------------------------

var app = builder.Build();

// ---------------------------------------------------------
// 5️⃣ DATABASE CREATION AT STARTUP
// ---------------------------------------------------------

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GiteaContext>();
    db.Database.EnsureCreated(); // Automatically creates site.db if not exists
}

// ---------------------------------------------------------
// 6️⃣ MIDDLEWARE PIPELINE
// ---------------------------------------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // enable wwwroot (for CSS/JS)
app.UseRouting();
app.UseAuthorization();

// ---------------------------------------------------------
// 7️⃣ MVC ROUTING
// ---------------------------------------------------------

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Init}/{action=Index}/{id?}"
);

// ---------------------------------------------------------
// 8️⃣ RUN
// ---------------------------------------------------------

app.Run();
