CREATE OR REPLACE FUNCTION fn_review_get_all()
RETURNS TABLE
(
    "Id" INT,
    "UserId" UUID,
    "ListingId" INT,
    "BookingId" INT,
    "Comment" TEXT,
    "Rating" INT,
    "Cleanliness" INT,
    "Accuracy" INT,
    "CheckIn" INT,
    "Communication" INT,
    "Location" INT,
    "Value" INT,
    "Overall" INT,
    "CreatedAt" TIMESTAMP
)
LANGUAGE sql
AS $$
    SELECT
        r.id,
        r.user_id,
        r.listing_id,
        r.booking_id,
        r.comment,
        r.rating,
        r.cleanliness,
        r.accuracy,
        r.check_in,
        r.communication,
        r.location_rating,
        r.value,
        r.overall,
        r.created_at
    FROM review r
    ORDER BY r.created_at DESC;
$$;