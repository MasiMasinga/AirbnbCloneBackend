/*
Author: Masi Masinga
Date: 2026-07-29
Description: This script fetches all the entries from the Listings Table
*/

CREATE OR REPLACE FUNCTION listings_get_all()
RETURNS TABLE
(
    id INTEGER,
    title VARCHAR,
    description VARCHAR,
    pricing NUMERIC
)
LANGUAGE sql
AS
$$
SELECT
    l.id,
    l.title,
    l.description,
    l.pricing
FROM listings l;
$$;