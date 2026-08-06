/*
Author: Masi Masinga
Date: 2026-07-29
Description: This script fetches an entry from the Listings Table
*/

DROP FUNCTION IF EXISTS listing_get_by_id(INTEGER);

CREATE OR REPLACE FUNCTION listing_get_by_id
(
    listing_id INTEGER
)
RETURNS TABLE
(
    id INTEGER,
    title VARCHAR,
    description VARCHAR,
    amenities VARCHAR,
    house_rules VARCHAR,
    pricing NUMERIC,
    availability BOOLEAN,
    bed_count INTEGER,
    bath_count INTEGER,
    property_type VARCHAR,
    address VARCHAR,
    location VARCHAR,
    country VARCHAR,
    photo TEXT,
    user_id UUID
)
LANGUAGE sql
AS
$$
SELECT
    l.id,
    l.title,
    l.description,
    COALESCE((
        SELECT string_agg(a.name, ', ' ORDER BY a.name)::VARCHAR
        FROM listing_amenities la
        JOIN amenities a ON a.id = la.amenity_id
        WHERE la.listing_id = l.id
    ), '')::VARCHAR AS amenities,
    l.house_rules,
    l.pricing,
    l.availability,
    l.bed_count,
    l.bath_count,
    l.property_type,
    l.address,
    loc.city AS location,
    loc.country,
    COALESCE((
        SELECT lp.url
        FROM listing_photos lp
        WHERE lp.listing_id = l.id
        ORDER BY lp.sort_order, lp.id
        LIMIT 1
    ), '') AS photo,
    l.user_id
FROM listings l
JOIN locations loc ON loc.id = l.location_id
WHERE l.id = listing_id;
$$;
