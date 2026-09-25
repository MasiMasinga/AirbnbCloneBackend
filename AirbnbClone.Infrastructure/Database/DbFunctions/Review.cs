namespace AirbnbClone.Infrastructure.Database;

public static partial class DbFunctions
{
    public static class Review
    {
        public const string GetAll = "SELECT * FROM fn_review_get_all();";
        public const string GetById = "SELECT * FROM fn_review_get_by_id(@id);";
        public const string Create =
            """
            SELECT * FROM fn_review_create(
                @userId, 
                @listingId, 
                @bookingId, 
                @comment, 
                @rating, 
                @cleanLiness, 
                @accuracy, 
                @checkIn, 
                @communication, 
                @location, 
                @value, 
                @overall, 
                @createdAt
            );
            """;
        public const string Delete = "SELECT * FROM fn_review_delete(@id);";
    }
}
