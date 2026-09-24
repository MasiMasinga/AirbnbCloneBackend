CREATE OR REPLACE FUNCTION fn_booking_get_by_id(p_id INTEGER)
RETURNS SETOF booking
LANGUAGE sql
STABLE
AS
$$
    SELECT *
    FROM booking
    WHERE id = p_id;
$$;