using AirbnbClone.Application.Common.Abstractions;
using AirbnbClone.Application.Features.Listings.Interfaces;
using AirbnbClone.Application.Features.Listings.Services;
using AirbnbClone.Application.Features.Listings.Validators;
using AirbnbClone.Infrastructure;
using AirbnbClone.Infrastructure.Logging;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

builder.Services.AddScoped<IListingService, ListingService>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateListingValidator>();

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "swagger";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AirbnbClone v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();