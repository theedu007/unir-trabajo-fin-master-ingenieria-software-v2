using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ScrumBoard.Common.Extensions.WebBuilderExtensions;
using ScrumBoard.Common.Identity;
using OpenIddict.Abstractions;
using ScrumBoard.BackEnd.Services;
using ScrumBoard.Common.Application;
using ScrumBoard.Common.Helpers;

namespace ScrumBoard.BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args)
                .AddLocalAppSettings();
            var configuration = builder.Configuration;

            //MongoDb class mapper
            BsonClassMapper.MapClasses();
            builder.Services.AddNamedHttpClients(configuration);
            builder.Services.AddHttpContextAccessor();
            builder.Services.Configure<ApplicationDbSettings>(options =>
                configuration.GetSection("ApplicationDbSettings").Bind(options));

            builder.Services.AddTransient<ApplicationDbContext>();
            builder.Services.AddTransient<WorkspaceUiService>();

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
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudiences = new List<string>()
                    {
                        "apitest",
                        "ScrumBoardFrontendApp",
                        "Authorization",
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