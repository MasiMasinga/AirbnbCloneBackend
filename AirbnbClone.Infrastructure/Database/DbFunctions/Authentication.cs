namespace AirbnbClone.Infrastructure.Database;

public static partial class DbFunctions
{
    public static class Authentication
    {
        public const string SignUp =
            """
            SELECT create_user(
                @auth_user_role,
                @auth_user_title,
                @auth_user_firstname,
                @auth_user_email_address,
                @auth_user_surname,
                @auth_user_bio,
                @auth_user_photo,
                @auth_user_password_hash
            );
            """;
        
        public const string Login = 
            "SELECT login_user(@auth_user_email);";

        public const string CheckExistingUser =
            """
            SELECT
                id,
                user_role,
                title,
                first_name,
                surname,
                email_address,
                bio,
                photo,
                password_hash AS password
            FROM users
            WHERE LOWER(email_address) = @auth_user_email;
            """;

    }
}
