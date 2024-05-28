using Ecommerce.Data; 
using Ecommerce.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Retrieve MongoDB connection settings from appsettings.json
var mongoDbSettings = builder.Configuration.GetSection("MongoDBSettings");
var connectionString = mongoDbSettings["ConnectionString"];
var databaseName = mongoDbSettings["DatabaseName"];

// Validate the connection string and database name
if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentException("MongoDB connection string is not configured.", nameof(connectionString));
}

if (string.IsNullOrEmpty(databaseName))
{
    throw new ArgumentException("MongoDB database name is not configured.", nameof(databaseName));
}

// Register MongoDB context
builder.Services.AddSingleton(new MongoDbContext(connectionString, databaseName));

// Register repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddSingleton<BookRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Add the route for FetchBook action
app.MapControllerRoute(
    name: "FetchByGenre",
    pattern: "Book/FetchByGenre/{genre}",
    defaults: new { controller = "Book", action = "FetchByGenre" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
