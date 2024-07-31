using HospitalDomain.Entites.Identity;
using HospitalInfrastructure.AppContext;
using HospitalInfrastructure.IdentityContext;
using HospitalAPP.ServicesExtension;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HospitalAPP.Email.WorkEmail;
using HospitalDomain.DTOS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalAppAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure the database context for Hospital
            builder.Services.AddDbContext<HospitalContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Configure the database context for Identity
            builder.Services.AddDbContext<AccountContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppIdentityConnection")))
               ;

            builder.Services.AddDefaultIdentity<Account>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>() // Add roles
            .AddEntityFrameworkStores<AccountContext>()
            .AddDefaultTokenProviders();

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

            builder.Services.AddIdentityCore<Guest>(options =>
            {
            }).AddRoles<IdentityRole>() // Add roles
                .AddEntityFrameworkStores<AccountContext>()
                .AddSignInManager<SignInManager<Guest>>()
                .AddDefaultTokenProviders();
            builder.Services.AddIdentityCore<Employee>(options =>
            {
            }).AddRoles<IdentityRole>() // Add roles
          .AddEntityFrameworkStores<AccountContext>()
          .AddSignInManager<SignInManager<Employee>>()
          .AddDefaultTokenProviders();

            builder.Services.AddAplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Guest", "Employee" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
            app.Run();
        }
    }
}