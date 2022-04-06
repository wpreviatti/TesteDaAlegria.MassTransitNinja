using MassTransit;
using MassTransit.Mediator;
using TesteDaAlegria.MassTransitNinja.Components.Consumers;
using TesteDaAlegria.MassTransitNinja.Contracts.Order;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(cfg =>
{
    cfg.AddConsumer<SubmitOrderConsumer>();
    cfg.AddMediator();
    cfg.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
    
    cfg.AddRequestClient<SubmitOrder>();
});

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
