using BussinessLogic.Services.Implementations;
using BussinessLogic.Middlewares;
using BussinessLogic.Extensions;

namespace BussinessLogic;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddApi();
        builder.AddServices();

        builder.AddCorsConfiguration();
        builder.AddSwaggerAuthorization("Fridge App");
        builder.ConfigureAuthentication();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseCors();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}
