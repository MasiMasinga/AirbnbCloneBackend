namespace AirbnbClone.Infrastructure.Database;

public static partial class DbFunctions
{
    public static class Users
    {
        public const string GetUserDetails = 
            "SELECT * FROM fn_get_user_details_by_id(@Id);";
        public const string UpdateUserDetails = 
            "SELECT * FROM fn_update_user_details(@Id, @UserRole, @Title, @FirstName, @Surname, @EmailAddress, @Bio, @Photo, @PasswordHash);";
        public const string DeleteUser = "SELECT fn_delete_user(@Id);";
    }
}
