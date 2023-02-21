using StockApi.RabbitMQ;
using StockApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBotService, BotService>();
builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", option =>
    {
        option.AllowAnyMethod().AllowAnyHeader()
        .WithOrigins("http://localhost:4200")
        .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
