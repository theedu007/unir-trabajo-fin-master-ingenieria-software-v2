using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ScrumBoard.Common.Extensions.WebBuilderExtensions;
using ScrumBoard.Common.Identity;
using OpenIddict.Abstractions;

namespace ScrumBoard.BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args)
                .AddLocalAppSettings();
            var configuration = builder.Configuration;
            // Add services to the container.

            builder.Services.AddDbContext<ApplicationIdentityDbContext>(config =>
            {
                config.UseSqlServer(configuration.GetConnectionString("IdentityDb"),
                    x => x.MigrationsAssembly("ScrumBoard.Common"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(x => 
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:5001";
                options.Audience = "Metadata";

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudiences = new List<string>()
                    {
                        "apitest",
                        "ScrumBoardFrontendApp"
                    }
                };
            });
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