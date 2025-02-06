
using LMS.Core.Data;
using LMS.Core.Repository;
using LMS.Core.service;
using LMS.Infra.Repository;
using LMS.Infra.service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LMS.API
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

            builder.Services.AddDbContext<LmsdemoDbContext>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IAuthRepo, AuthRepo>();


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                 };
               });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
