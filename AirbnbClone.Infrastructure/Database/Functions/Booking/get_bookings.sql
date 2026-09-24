CREATE OR REPLACE FUNCTION fn_booking_get_all()
RETURNS SETOF booking
LANGUAGE sql
STABLE
AS
$$
    SELECT *
    FROM booking
    ORDER BY id;
$$;
