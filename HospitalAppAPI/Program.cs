using HospitalDomain.Entites.Identity;
using HospitalInfrastructure.AppContext;
using HospitalInfrastructure.IdentityContext;
using HospitalAPP.ServicesExtension;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace HospitalAppAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<HospitalContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Configure the database context
            builder.Services.AddDbContext<AccountContext>(options =>
                  options.UseSqlServer(builder.Configuration.GetConnectionString("AppIdentityConnection")));

            // Configure Identity for Account with default UI
            builder.Services.AddDefaultIdentity<Account>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>() // Add roles if necessary
                .AddEntityFrameworkStores<AccountContext>();

            // Configure Identity for Employee
            builder.Services.AddIdentityCore<Employee>(options => { /* Employee-specific options */ })
                .AddEntityFrameworkStores<AccountContext>()
                .AddSignInManager<SignInManager<Employee>>(); // Add SignInManager if needed

            // Configure Identity for Guest
            builder.Services.AddIdentityCore<Guest>(options => { /* Guest-specific options */ })
                .AddEntityFrameworkStores<AccountContext>()
                .AddSignInManager<SignInManager<Guest>>(); // Add SignInManager if needed

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

            app.Run();
        }
    }
}