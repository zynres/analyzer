using Analyzer.Html.Services.Repositories;
using Analyzer.Html.Services.Validators;
using Analyzer.Html.Services;
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

        builder.Services.AddSingleton<EmailExtractorService>();
        builder.Services.AddSingleton<ElementParserService>();
        builder.Services.AddSingleton<HtmlParserService>();
        builder.Services.AddSingleton<AesService>();

        builder.Services.AddScoped<ElementProcessingService>();
        builder.Services.AddScoped<ElementRepository>();

        var app = builder.Build();

        app.MapSwagger();
        app.MapSwaggerUI(setupAction: setup =>
        {
            setup.RoutePrefix = "api/swagger";
            setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Analyzer");
        });

        DbInitializer.Initialize(app.Configuration);

        app.MapControllers();

        app.Run();
    }
}
