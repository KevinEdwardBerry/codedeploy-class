var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        {
            var temperature = Random.Shared.Next(-20, 55);
            var description = temperature switch
            {
                var x when x <= 0 => "Freezing",
                var x when x >  0 && x <= 10 => "Bracing",
                var x when x > 10 && x <= 15 => "Chilly",
                var x when x > 15 && x <= 20 => "Cool",
                var x when x > 20 && x <= 23 => "Mild",
                var x when x > 23 && x <= 27 => "Warm",
                var x when x > 27 && x <= 30 => "Balmy",
                var x when x > 30 && x <= 40 => "Hot",
                var x when x > 40 && x <= 45 => "Sweltering",
                _ => "Scorching"
            };

            return new WeatherForecast(
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                temperature,
                description
            );
        })
        .ToArray();

    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
