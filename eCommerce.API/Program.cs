using eCommerce.Infrastructure;
using eCommerce.Core;
using eCommerce.API.Middlewares;
using System.Text.Json.Serialization;
using eCommerce.Core.Mappers;
using FluentValidation.AspNetCore;

namespace eCommerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddInfrastructure();
            builder.Services.AddCore();

            //Add controllers to the services collection
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            //builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);
            builder.Services.AddAutoMapper(config => { }, typeof(ApplicationUserMappingProfile).Assembly);
            //builder.Services.AddAutoMapper(typeof(RegisterRequestMappingProfile).Assembly);

            //FluentValidations
            builder.Services.AddFluentValidationAutoValidation();

            //Build the web application
            var app = builder.Build();

            //Use custom exception handling middleware
            app.UseExceptionHandlingMiddleware();

            //Routing
            app.UseRouting();

            //Auth
            app.UseAuthentication();
            app.UseAuthorization();

            //Controller routes
            app.MapControllers();

            app.Run();
        }
    }
}
