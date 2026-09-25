using Analyzer.Html.Services.Validators;
using FluentValidation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddValidatorsFromAssemblyContaining<PostElementRequestValidator>();

        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapSwagger();
            app.MapSwaggerUI(setupAction: setup =>
            {
                setup.RoutePrefix = "api/swagger";
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Analyzer");
            });
        }
        else
        {
            app.UseHttpsRedirection();
        }

        app.MapControllers();

        app.Run();
    }
}
