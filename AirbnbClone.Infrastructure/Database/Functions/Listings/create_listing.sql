/*
Author: Masi Masinga
Date: 2026-07-29
Description: This script does an insertion to the Listings Table
             (amenities -> listing_amenities, photo -> listing_photos,
              location/country -> locations.location_id)
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
       loc_id INTEGER;
       amenity_name TEXT;
       amenity_pk INTEGER;
BEGIN
    -- start_date / end_date are accepted for API compatibility but are no longer stored on listings

    SELECT id INTO loc_id
    FROM locations
    WHERE city = listing_location
      AND country = listing_country;

    IF loc_id IS NULL THEN
        INSERT INTO locations (city, country)
        VALUES (listing_location, listing_country)
        RETURNING id INTO loc_id;
    END IF;

    INSERT INTO listings
    (
        title,
        description,
        house_rules,
        pricing,
        availability,
        bed_count,
        bath_count,
        property_type,
        address,
        location_id,
        user_id
    )
    VALUES
    (
        listing_title,
        listing_description,
        listing_house_rules,
        listing_pricing,
        listing_availability,
        listing_bed_count,
        listing_bath_count,
        listing_property_type,
        listing_address,
        loc_id,
        listing_user_id
    )
    RETURNING id INTO new_id;

    IF listing_amenities IS NOT NULL AND btrim(listing_amenities) <> '' THEN
        FOREACH amenity_name IN ARRAY string_to_array(listing_amenities, ',')
        LOOP
            amenity_name := btrim(amenity_name);
            IF amenity_name = '' THEN
                CONTINUE;
            END IF;

            SELECT id INTO amenity_pk
            FROM amenities
            WHERE name = amenity_name;

            IF amenity_pk IS NULL THEN
                INSERT INTO amenities (name)
                VALUES (amenity_name)
                RETURNING id INTO amenity_pk;
            END IF;

            INSERT INTO listing_amenities (listing_id, amenity_id)
            VALUES (new_id, amenity_pk)
            ON CONFLICT DO NOTHING;
        END LOOP;
    END IF;

    IF listing_photo IS NOT NULL AND btrim(listing_photo) <> '' THEN
        INSERT INTO listing_photos (listing_id, url, sort_order)
        VALUES (new_id, listing_photo, 0);
    END IF;

    RETURN new_id;
END;
$$;
