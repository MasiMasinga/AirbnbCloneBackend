CREATE OR REPLACE FUNCTION fn_review_create(
    review_user_id UUID,
    review_listing_id INT,
    review_booking_id INT,
    review_comment TEXT,
    review_rating INT,
    review_cleanliness INT,
    review_accuracy INT,
    review_check_in INT,
    review_communication INT,
    review_location INT,
    review_value INT,
    review_overall INT,
    review_created_at TIMESTAMP
)
RETURNS INT
LANGUAGE sql
AS $$
    INSERT INTO review
    (
        user_id,
        listing_id,
        booking_id,
        comment,
        rating,
        cleanliness,
        accuracy,
        check_in,
        communication,
        location_rating,
        value,
        overall,
        created_at
    )
    VALUES
    (
        review_user_id,
        review_listing_id,
        review_booking_id,
        review_comment,
        review_rating,
        review_cleanliness,
        review_accuracy,
        review_check_in,
        review_communication,
        review_location,
        review_value,
        review_overall,
        review_created_at
    )
    RETURNING id;
$$;