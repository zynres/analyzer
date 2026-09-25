var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapSwagger();
    app.MapSwaggerUI(setupAction: setup =>
    {
        setup.RoutePrefix = "api/swagger";
        setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Analyzer V1");
    });
}
else
{
    app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();
