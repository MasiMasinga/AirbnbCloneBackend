/*
Author: Masi Masinga
Date: 2026-07-29
Description: This script does an insertion to the Listings Table
*/

CREATE OR REPLACE FUNCTION create_listing
(
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
    listing_photo TEXT,
    listing_user_id UUID
)
       RETURNS INTEGER
       LANGUAGE plpgsql
AS
$$
DECLARE
       new_id INTEGER;
BEGIN

INSERT INTO listings
(
    title,
    description,
    amenities,
    house_rules,
    pricing,
    availability,
    bed_count,
    bath_count,
    property_type,
    address,
    location,
    country,
    start_date,
    end_date,
    photo,
    user_id
)
VALUES
    (
        listing_title,
        listing_description,
        listing_amenities,
        listing_house_rules,
        listing_pricing,
        listing_availability,
        listing_bed_count,
        listing_bath_count,
        listing_property_type,
        listing_address,
        listing_location,
        listing_country,
        listing_start_date,
        listing_end_date,
        listing_photo,
        listing_user_id
    )
    RETURNING id INTO new_id;
RETURN new_id;

END;
$$;