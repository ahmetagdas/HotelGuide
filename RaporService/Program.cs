using Data.Entity;
using MassTransit;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<OtelDbContext>(options =>
{
    // TODO: modify connection string usage like below and make relevant changes to appconfig files
    //var connectionString = builder.Configuration.GetConnectionString("PostgreSql");
    var connectionString = builder.Configuration["Database:ConnectionString"];
    options.UseNpgsql(connectionString);

    options.ConfigureWarnings(warningsConfigurationBuilder =>
    {
        warningsConfigurationBuilder.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning);
    });
});

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost"); // RabbitMQ baðlantý bilgileri
    });
});

builder.Services.AddMassTransitHostedService(); // MassTransit'ý çalýþtýrýyoruz


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
