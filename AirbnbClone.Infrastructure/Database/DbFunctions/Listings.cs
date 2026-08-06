namespace AirbnbClone.Infrastructure.Database;

public static partial class DbFunctions
{
    public static class Listings
    {
        public const string GetAll =
            "SELECT * FROM listings_get_all();";

        public const string GetById =
            """
            SELECT *
            FROM listing_get_by_id(@listing_id);
            """;

        public const string Create =
            """
            SELECT create_listing(
                @listing_title,
                @listing_description,
                @listing_amenities,
                @listing_house_rules,
                @listing_pricing,
                @listing_availability,
                @listing_bed_count,
                @listing_bath_count,
                @listing_property_type,
                @listing_address,
                @listing_location,
                @listing_country,
                @listing_start_date,
                @listing_end_date,
                @listing_photo,
                @listing_user_id
            );
            """;

        public const string Update =
            """
            SELECT listings_update(
                @listing_id,
                @listing_title,
                @listing_description,
                @listing_amenities,
                @listing_house_rules,
                @listing_pricing,
                @listing_availability,
                @listing_bed_count,
                @listing_bath_count,
                @listing_property_type,
                @listing_address,
                @listing_location,
                @listing_country,
                @listing_start_date,
                @listing_end_date,
                @listing_photo
            );
            """;

        public const string Delete =
            "SELECT delete_listing(@listing_id);";
    }
}
