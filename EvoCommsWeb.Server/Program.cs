using EvoCommsWeb.Server.Auth;
using EvoCommsWeb.Server.Database;
using EvoCommsWeb.Server.Terminals.ZK;
using EvoCommsWeb.Server.Terminals.ZK.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? "Data Source=app.db";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings (example)
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    // Lockout settings (example)
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;
});
builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton<ServerSettings>();
builder.Services.AddScoped<ZkService>();
builder.Services.AddControllers();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/account/login"; // Path for login
    options.LogoutPath = "/api/account/logout"; // Path for logout
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure HTTPS
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.Name = "evocomms-session";
    options.ExpireTimeSpan = TimeSpan.FromHours(48);
    options.SlidingExpiration = true;
});

builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// if development
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowViteDevServer",
            policy =>
            {
                policy.WithOrigins("https://localhost:54471/") // React development server
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials(); // Allow cookies to be sent
            });
    });
}

var app = builder.Build();
app.UseMiddleware<ZkLoggingMiddleware>();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowViteDevServer");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
