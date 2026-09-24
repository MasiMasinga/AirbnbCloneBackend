namespace AirbnbClone.Infrastructure.Database;

public static partial class DbFunctions
{
    public static class Bookings
    {
        public const string GetAll = "SELECT * FROM fn_booking_get_all();";
        public const string GetById = "SELECT * FROM fn_booking_get_by_id(@id);";
        public const string Create = "SELECT * FROM fn_booking_create(@listingId, @userId, @startDate, @endDate, @numberOfGuests, @amount);";
        public const string Update = "SELECT * FROM fn_booking_update(@id, @listingId, @userId, @startDate, @endDate, @numberOfGuests, @amount);";
        public const string Delete = "SELECT * FROM fn_booking_delete(@id);";
    }
}
