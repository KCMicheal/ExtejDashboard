using ExtejDashboard.Components;
using ExtejDashboard_Domain.ExtejDashboardContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string? origins = "origins";
// Add services to the container.
builder.Services.AddRazorComponents();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ExtejDashboard")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(origins,
        policy =>
        {
            policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(origins);

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
