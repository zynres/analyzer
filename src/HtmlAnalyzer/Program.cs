using HtmlAnalyzer.Services.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapSwagger();
    app.MapSwaggerUI(setupAction: setup =>
    {
        setup.RoutePrefix = "api/swagger";
        setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Analyzer");
    });

    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
    context.Database.Migrate();
}
else
{
    app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();
