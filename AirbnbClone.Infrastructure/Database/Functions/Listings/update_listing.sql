/*
Author: Masi Masinga
Date: 2026-07-29
Description: This script updates an entry from the Listings Table
*/

CREATE OR REPLACE FUNCTION listings_update
(
    listing_id INTEGER,
    listing_title VARCHAR,
    listing_description VARCHAR,
    listing_amenities VARCHAR,
    listing_house_rules VARCHAR,
    listing_pricing NUMERIC,
    listing_availability BOOLEAN,
    listing_bed_count INTEGER,
    listing_bath_count INTEGER,
    listing_property_type VARCHAR,
    listing_address VARCHAR,
    listing_location VARCHAR,
    listing_country VARCHAR,
    listing_start_date DATE,
    listing_end_date DATE,
    listing_photo TEXT
)
       RETURNS VOID
       LANGUAGE plpgsql
AS
$$
BEGIN

UPDATE listings
SET
    title = listing_title,
    description = listing_description,
    amenities = listing_amenities,
    house_rules = listing_house_rules,
    pricing = listing_pricing,
    availability = listing_availability,
    bed_count = listing_bed_count,
    bath_count = listing_bath_count,
    property_type = listing_property_type,
    address = listing_address,
    location = listing_location,
    country = listing_country,
    start_date = listing_start_date,
    end_date = listing_end_date,
    photo = listing_photo
WHERE id = listing_id;

END;
$$;