CREATE OR REPLACE FUNCTION fn_booking_delete(p_id INTEGER)
RETURNS BOOLEAN
LANGUAGE sql
AS
$$
    WITH deleted AS (
        DELETE FROM booking
        WHERE id = p_id
        RETURNING id
    )
    SELECT EXISTS (
        SELECT 1
        FROM deleted
    );
$$;