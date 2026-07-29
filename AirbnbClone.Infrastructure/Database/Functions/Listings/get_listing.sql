/*
Author: Masi Masinga
Date: 2026-07-29
Description: This script fetches an entry from the Listings Table
*/

CREATE OR REPLACE FUNCTION listing_get_by_id
(
    listing_id INTEGER
)
RETURNS TABLE
(
    id INTEGER,
    name VARCHAR,
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
FROM listings l
WHERE l.id = listing_id;
$$;