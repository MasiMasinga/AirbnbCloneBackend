using System.Data;
using Dapper;
using AirbnbClone.Domain.Interfaces;
using AirbnbClone.Infrastructure.Database;
using AirbnbClone.Domain.Entities;

namespace AirbnbClone.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly IDbConnection _connection;

    public BookingRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public Task<IEnumerable<Booking>> GetAllAsync(CancellationToken ct = default)
    {
        return _connection.QueryAsync<Booking>(
            new CommandDefinition(
                DbFunctions.Bookings.GetAll,
                cancellationToken: ct));
    }

    public Task<Booking?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return _connection.QuerySingleOrDefaultAsync<Booking>(
            new CommandDefinition(
                DbFunctions.Bookings.GetById,
                new
                {
                    id = id
                },
                cancellationToken: ct));
    }

    public Task<int> CreateAsync(Booking booking, CancellationToken ct = default)
    {
        return _connection.ExecuteScalarAsync<int>(
            new CommandDefinition(
                DbFunctions.Bookings.Create,
                new
                {
                    listingId = booking.ListingId,
                    userId = booking.UserId,
                    startDate = booking.StartDate,
                    endDate = booking.EndDate,
                    numberOfGuests = booking.NumberOfGuests,
                    amount = booking.Amount
                },
                cancellationToken: ct));
    }

    public Task UpdateAsync(Booking booking, CancellationToken ct = default)
    {
        return _connection.ExecuteAsync(
            new CommandDefinition(
                DbFunctions.Bookings.Update,
                new
                {
                    id = booking.Id,
                    listingId = booking.ListingId,
                    userId = booking.UserId,
                    startDate = booking.StartDate,
                    endDate = booking.EndDate,
                    numberOfGuests = booking.NumberOfGuests,
                    amount = booking.Amount
                },
                cancellationToken: ct));
    }

    public Task DeleteAsync(int id, CancellationToken ct = default)
    {
        return _connection.ExecuteAsync(
            new CommandDefinition(
                DbFunctions.Bookings.Delete,
                new
                {
                    id = id
                },
                cancellationToken: ct));
    }
}