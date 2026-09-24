CREATE OR REPLACE FUNCTION fn_booking_create(
    p_listing_id INTEGER,
    p_user_id UUID,
    p_start_date TIMESTAMPTZ,
    p_end_date TIMESTAMPTZ,
    p_number_of_guests INTEGER,
    p_amount NUMERIC
)
RETURNS INTEGER
LANGUAGE sql
AS
$$
    INSERT INTO booking (
        listing_id,
        user_id,
        start_date,
        end_date,
        number_of_guests,
        amount
    )
    VALUES (
        p_listing_id,
        p_user_id,
        p_start_date,
        p_end_date,
        p_number_of_guests,
        p_amount
    )
    RETURNING id;
$$;