using ExtejDashboard.Components;
using ExtejDashboard_Domain.Enums;
using ExtejDashboard_Domain.ExtejDashboardContext;
using ExtejDashboard_Domain.Models;
using ExtejDashboard_Services.Services;
using ExtejDashboard_Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string? origins = "origins";
// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ExtejDashboard")));

builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductService, ProductService>();

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

// Seed the database with default products
await SeedDatabase(app);

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();

async Task SeedDatabase(WebApplication app)
{
	using var scope = app.Services.CreateScope();
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();


	dbContext.Products.RemoveRange(dbContext.Products);
	dbContext.CartItems.RemoveRange(dbContext.CartItems);
	await dbContext.SaveChangesAsync();


	var defaultProducts = new List<Product>
	{
		new Product { Id = Guid.NewGuid(), Name = "Laptop", Description = "High-performance laptop", Price = 999.99m, Category = ProductCategory.Electronics },
		new Product { Id = Guid.NewGuid(), Name = "Smartphone", Description = "Latest smartphone model", Price = 799.99m,  Category = ProductCategory.Electronics},
		new Product { Id = Guid.NewGuid(), Name = "Body Cream", Description = "Body cream for adults", Price = 199.99m, Category = ProductCategory.Lifestyle },
		new Product { Id = Guid.NewGuid(), Name = "Toy car", Description = "Toy car for kids 5-12", Price = 5.99m, Category = ProductCategory.Kiddies }
	};

	dbContext.Products.AddRange(defaultProducts);
	await dbContext.SaveChangesAsync();
}
