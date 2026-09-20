/*
    Author: Masi Masinga
    Date: 2026-09-20
    Description: This script deletes a entry from the Users Table
*/

CREATE OR REPLACE FUNCTION fn_delete_user(p_id UUID)
RETURNS BOOLEAN
LANGUAGE sql
AS
$$
DELETE FROM users
WHERE id = p_id
    RETURNING TRUE;
$$;