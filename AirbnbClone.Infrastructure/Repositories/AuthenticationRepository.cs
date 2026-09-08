using System.Data;
using AirbnbClone.Domain.Entities;
using AirbnbClone.Domain.Interfaces;
using AirbnbClone.Infrastructure.Database;
using Dapper;

namespace AirbnbClone.Infrastructure.Repositories;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly IDbConnection _connection;

    public AuthenticationRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    
     public async Task<Guid> SignUp(User user, CancellationToken ct = default)
    {
        return await _connection.ExecuteScalarAsync<Guid>(
            new CommandDefinition(DbFunctions.Authentication.SignUp, new
            {
                auth_user_role = user.UserRole,
                auth_user_title = user.Title,
                auth_user_firstname = user.FirstName,
                auth_user_surname = user.Surname,
                auth_user_email_address = user.EmailAddress,
                auth_user_bio = user.Bio,
                auth_user_photo = user.Photo,
                auth_user_password_hash = user.Password
            }, cancellationToken: ct));
    }

    public async Task Login(User user, CancellationToken ct = default)
    {
        await _connection.ExecuteAsync(
            new CommandDefinition(DbFunctions.Authentication.Login, new
            {
                auth_user_email_address = user.EmailAddress,
            }, cancellationToken: ct));
    }

    public async Task<User?> CheckExistingUser(string emailAdress, CancellationToken ct = default)
    {
        return await _connection.QuerySingleOrDefaultAsync<User>(
            new CommandDefinition(DbFunctions.Authentication.CheckExistingUser, 
                new 
                {
                    auth_user_email = emailAdress 
                }, cancellationToken: ct));
    }
}