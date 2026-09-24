using System.Text;
using AirbnbClone.Application.Common.Abstractions;
using AirbnbClone.Application.Features.Authentication.Interfaces;
using AirbnbClone.Application.Features.Authentication.Services;
using AirbnbClone.Application.Features.Authentication.Validators;
using AirbnbClone.Application.Features.Listings.Interfaces;
using AirbnbClone.Application.Features.Listings.Services;
using AirbnbClone.Application.Features.Listings.Validators;
using AirbnbClone.Application.Features.User.Interfaces;
using AirbnbClone.Application.Features.User.Services;
using AirbnbClone.Application.Features.Booking.Services;
using AirbnbClone.Application.Features.Booking.Interfaces;
using AirbnbClone.Application.Features.Booking.Validators;
using AirbnbClone.Infrastructure;
using AirbnbClone.Infrastructure.Logging;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = !string.IsNullOrWhiteSpace(builder.Configuration["Jwt:Issuer"]),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = !string.IsNullOrWhiteSpace(builder.Configuration["Jwt:Audience"]),
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });
});

builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins(builder.Configuration.GetSection("Cors:Origins").Get<string[]>())
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateListingValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginUserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookingValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateBookingValidator>();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapGet("/", () => "🚀 Airbnb Clone Backend is running!");

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
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();