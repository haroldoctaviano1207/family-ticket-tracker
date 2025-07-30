
using HO.FamilyTicketTracker.API.Data;
using HO.FamilyTicketTracker.API.Models;
using HO.FamilyTicketTracker.API.Services;
using HO.FamilyTicketTracker.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using HO.FamilyTicketTracker.API.Repository;

namespace HO.FamilyTicketTracker.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      builder.Services.AddControllers();
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "Family Ticket Tracker API (FTT)",
          Version = "v1",
          Description = "An API for managing family tickets and related data."
        });

        c.AddServer(new OpenApiServer { Url = "/ftt" });
      });

      var connectionString = builder.Configuration.GetConnectionString("sqlConn");
      builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

      //Add CORS
      builder.Services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
        {
          builder.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod();
        });
      });

      builder.Services.AddIdentity<User, IdentityRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

      // Add JWT Authentication
      var jwtSettings = builder.Configuration.GetSection("JwtSettings");
      var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"] ?? "default");

      builder.Services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ClockSkew = TimeSpan.Zero
        };
      });

      builder.Services.AddScoped<IUserRepository, UserRepository>();
      builder.Services.AddScoped<ILookUpRepository, LookUpRepository>();
      builder.Services.AddScoped<ITicketRepository, TicketRepository>();
      builder.Services.AddScoped<ICommentRepository, CommentRepository>();
      builder.Services.AddScoped<AuthService>();

      var app = builder.Build();

      app.UseCors();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseStaticFiles();

      using (var scope = app.Services.CreateScope())
      {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        SeedData(userManager, roleManager).GetAwaiter().GetResult();
      }
      app.UseSwagger();
      app.UseSwaggerUI();

      app.MapGroup("/ftt").MapControllers();

      app.Run();
    }

    static async Task SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
      string[] roleNames = { "Parent", "Child" };
      foreach (var roleName in roleNames)
      {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
          await roleManager.CreateAsync(new IdentityRole(roleName));
        }
      }

      if (await userManager.FindByEmailAsync("parent@family.com") == null)
      {
        var parent = new User
        {
          UserName = "parent@family.com",
          Email = "parent@family.com",
          FirstName = "Parent",
          LastName = "User",
          Avatar = "",
          Role = RoleType.Parent.ToString()
        };

        await userManager.CreateAsync(parent, "Parent123!");
        await userManager.AddToRoleAsync(parent, "Parent");
      }
    }
  }
}
