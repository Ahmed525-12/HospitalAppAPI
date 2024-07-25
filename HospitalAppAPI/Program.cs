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
        public static void Main(string[] args)
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
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppIdentityConnection")));

            // Configure Identity for Account with default UI
            builder.Services.AddDefaultIdentity<Account>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>() // Add roles if necessary
                .AddEntityFrameworkStores<AccountContext>().AddDefaultTokenProviders();
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            // Configure Identity for Employee
            builder.Services.AddIdentityCore<Employee>(options => { })
                .AddEntityFrameworkStores<AccountContext>()
                .AddSignInManager<SignInManager<Employee>>()
                .AddDefaultTokenProviders();
            // Add SignInManager if needed

            // Configure Identity for Guest
            builder.Services.AddIdentityCore<Guest>(options => { })
                .AddEntityFrameworkStores<AccountContext>()
                .AddSignInManager<SignInManager<Guest>>()
                .AddDefaultTokenProviders(); // Add SignInManager if needed
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