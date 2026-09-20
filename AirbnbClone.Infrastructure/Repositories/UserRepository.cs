using System.Data;
using Dapper;
using AirbnbClone.Domain.Entities;
using AirbnbClone.Domain.Interfaces;
using AirbnbClone.Infrastructure.Database;

namespace AirbnbClone.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _connection;

    public UserRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _connection.QuerySingleOrDefaultAsync<User>(
            new CommandDefinition(
                DbFunctions.Users.GetUserDetails, 
                new 
                { 
                    Id = id,
                }, 
                cancellationToken: ct));
    }

    public async Task<bool> UpdateAsync(UserUpdate update, Guid id, CancellationToken ct = default)
    {
        return await _connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                DbFunctions.Users.UpdateUserDetails,
                new
                {
                    Id = id,
                    update.UserRole,
                    update.Title,
                    update.FirstName,
                    update.Surname,
                    update.EmailAddress,
                    update.Bio,
                    update.Photo,
                    update.PasswordHash
                },
                cancellationToken: ct));
    }
    
    public async Task<bool> DeleteAsync( Guid id, CancellationToken ct = default)
    {
        return await _connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                DbFunctions.Users.DeleteUser,
                new { Id = id },
                cancellationToken: ct));
    }
}
