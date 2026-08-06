using System.Data;
using AirbnbClone.Domain.Interfaces;
using AirbnbClone.Infrastructure.Database.TypeHandlers;
using AirbnbClone.Infrastructure.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace AirbnbClone.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        services.AddScoped<IDbConnection>(_ =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

            return new NpgsqlConnection(connectionString);
        });

        services.AddScoped<IListingRepository, ListingRepository>();

        return services;
    }
}

