CREATE OR REPLACE FUNCTION fn_booking_update(
    p_id INTEGER,
    p_listing_id INTEGER,
    p_user_id UUID,
    p_start_date TIMESTAMPTZ,
    p_end_date TIMESTAMPTZ,
    p_number_of_guests INTEGER,
    p_amount NUMERIC
)
RETURNS BOOLEAN
LANGUAGE sql
AS
$$
    WITH updated AS (
        UPDATE booking
        SET
            listing_id = p_listing_id,
            user_id = p_user_id,
            start_date = p_start_date,
            end_date = p_end_date,
            number_of_guests = p_number_of_guests,
            amount = p_amount
        WHERE id = p_id
        RETURNING id
    )
    SELECT EXISTS (
        SELECT 1
        FROM updated
    );
$$;