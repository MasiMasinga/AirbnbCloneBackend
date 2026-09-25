using System.Data;
using Dapper;
using AirbnbClone.Domain.Entities;
using AirbnbClone.Domain.Interfaces;
using AirbnbClone.Infrastructure.Database;

namespace AirbnbClone.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly IDbConnection _connection;

    public ReviewRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public Task<IEnumerable<Review>> GetAllAsync(CancellationToken ct = default)
    {
        return _connection.QueryAsync<Review>(
            new CommandDefinition(
                DbFunctions.Review.GetAll,
                cancellationToken: ct));
    }

    public Task<Review?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return _connection.QuerySingleOrDefaultAsync<Review>(
            new CommandDefinition(
                DbFunctions.Review.GetById,
                new
                {
                    id = id
                },
                cancellationToken: ct));
    }

    public Task<int> CreateAsync(Review review, CancellationToken ct = default)
    {
        return _connection.ExecuteScalarAsync<int>(
            new CommandDefinition(
                DbFunctions.Review.Create,
                new
                {
                    userId = review.UserId,
                    listingId = review.ListingId,
                    bookingId = review.BookingId,
                    comment = review.Comment,
                    rating = review.Rating,
                    cleanliness = review.Cleanliness,
                    accuracy = review.Accuracy,
                    checkIn = review.CheckIn,
                    communication = review.Communication,
                    location = review.Location,
                    value = review.Value,
                    overall = review.Overall,
                    createdAt = review.CreatedAt
                },
                cancellationToken: ct));
    }

    public Task DeleteAsync(int id, CancellationToken ct = default)
    {
        return _connection.ExecuteAsync(
            new CommandDefinition(
                DbFunctions.Review.Delete,
                new
                {
                    id = id
                },
                cancellationToken: ct));
    }
}