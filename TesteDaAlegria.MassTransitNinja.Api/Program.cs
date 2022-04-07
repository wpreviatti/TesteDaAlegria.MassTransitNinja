using MassTransit;
using MassTransit.Mediator;
using TesteDaAlegria.MassTransitNinja.Components.Consumers;
using TesteDaAlegria.MassTransitNinja.Contracts.Order;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region MassTransitConfigWebApi
var services = builder.Services;
services.AddSingleton(KebabCaseEndpointNameFormatter.Instance);
builder.Services.AddMassTransit(cfg =>
{
    cfg.UsingRabbitMq();
    cfg.AddRequestClient<SubmitOrder>();
});

services.AddOptions<MassTransitHostOptions>()
    .Configure(options =>
    {
        // if specified, waits until the bus is started before
        // returning from IHostedService.StartAsync
        // default is false
        options.WaitUntilStarted = true;

        // if specified, limits the wait time when starting the bus
        options.StartTimeout = TimeSpan.FromSeconds(10);

        // if specified, limits the wait time when stopping the bus
        options.StopTimeout = TimeSpan.FromSeconds(30);
    });
//services.AddMassTransitHostedService();
#endregion
//builder.Services.AddScoped<SubmitOrderConsumer>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
