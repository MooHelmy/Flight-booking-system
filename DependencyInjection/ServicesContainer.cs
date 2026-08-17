using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public static class ServicesContainer
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            SqlOption =>
             {
                 SqlOption.MigrationsAssembly(typeof(ServicesContainer).Assembly.FullName);
                 SqlOption.EnableRetryOnFailure();
             }
            ),
             ServiceLifetime.Scoped

             );

        // Identity
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // Password rules
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            // Lockout after failed attempts
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.AllowedForNewUsers = true;
            // User rules
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;   // خليها true وقت الإنتاج
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
        // JWT Authentication
        var jwtSettings = configuration.GetSection("Jwt");
        var secretKey = jwtSettings["SigningKey"]!;

        services.AddAuthentication(options =>
          {
              options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = jwtSettings["Issuer"],
                  ValidAudience = jwtSettings["Audience"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
              };
          });
        services.AddAuthorization();
        // Repositories
        // Repositories (للـ Entities البسيطة بس)
        services.AddScoped<IGeneric<Flight>, GenericRepo<Flight>>();

        // Application Services
        services.AddScoped<IFlightServices, FlightServices>();
        services.AddScoped<IFareClassServices, FareClassServices>();
        services.AddScoped<IQueueServices, QueueServices>();
        services.AddScoped<IHoldServices, HoldServices>();
        services.AddScoped<IBookingServices, BookingServices>();

        return services;
    }
}