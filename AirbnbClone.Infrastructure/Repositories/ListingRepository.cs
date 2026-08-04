using System.Data;
using AirbnbClone.Domain.Entities;
using AirbnbClone.Domain.Interfaces;
using AirbnbClone.Infrastructure.Database;
using Dapper;

namespace AirbnbClone.Infrastructure.Repositories;

public class ListingRepository : IListingRepository
{
    private readonly IDbConnection _connection;

    public ListingRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<Listings>> GetAllAsync(CancellationToken ct = default)
    {
        return await _connection.QueryAsync<Listings>(
            new CommandDefinition(DbFunctions.Listings.GetAll, cancellationToken: ct));
    }

    public async Task<Listings?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _connection.QuerySingleOrDefaultAsync<Listings>(
            new CommandDefinition(DbFunctions.Listings.GetById, new { listing_id = id }, cancellationToken: ct));
    }

    public async Task<int> CreateAsync(Listings listing, CancellationToken ct = default)
    {
        return await _connection.ExecuteScalarAsync<int>(
            new CommandDefinition(DbFunctions.Listings.Create, new
            {
                listing_title = listing.Title,
                listing_description = listing.Description,
                listing_amenities = listing.Amenities,
                listing_house_rules = listing.HouseRules,
                listing_pricing = listing.Pricing,
                listing_availability = listing.Availability,
                listing_bed_count = listing.BedCount,
                listing_bath_count = listing.BathCount,
                listing_property_type = listing.PropertyType,
                listing_address = listing.Address,
                listing_location = listing.Location,
                listing_country = listing.Country,
                listing_start_date = listing.StartDate,
                listing_end_date = listing.EndDate,
                listing_photo = listing.Photo,
                listing_user_id = listing.UserId
            }, cancellationToken: ct));
    }

    public async Task UpdateAsync(Listings listing, CancellationToken ct = default)
    {
        await _connection.ExecuteAsync(
            new CommandDefinition(DbFunctions.Listings.Update, new
            {
                listing_id = listing.Id,
                listing_title = listing.Title,
                listing_description = listing.Description,
                listing_amenities = listing.Amenities,
                listing_house_rules = listing.HouseRules,
                listing_pricing = listing.Pricing,
                listing_availability = listing.Availability,
                listing_bed_count = listing.BedCount,
                listing_bath_count = listing.BathCount,
                listing_property_type = listing.PropertyType,
                listing_address = listing.Address,
                listing_location = listing.Location,
                listing_country = listing.Country,
                listing_start_date = listing.StartDate,
                listing_end_date = listing.EndDate,
                listing_photo = listing.Photo
            }, cancellationToken: ct));
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        await _connection.ExecuteAsync(
            new CommandDefinition(DbFunctions.Listings.Delete, new { listing_id = id }, cancellationToken: ct));
    }
}
