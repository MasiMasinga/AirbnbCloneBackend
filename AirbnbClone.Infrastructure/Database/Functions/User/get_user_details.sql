/*
    Author: Masi Masinga
    Date: 2026-09-20
    Description: This script gets a entry from the Users Table using the Id
*/


CREATE OR REPLACE FUNCTION fn_get_user_details_by_id(p_id UUID)
RETURNS SETOF users
LANGUAGE sql
AS
$$
SELECT *
FROM users
WHERE id = p_id;
$$;