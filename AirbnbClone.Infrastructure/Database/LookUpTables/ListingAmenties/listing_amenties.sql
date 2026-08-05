/*
Author: Masi Masinga
Date: 2026-08-05
Description: This script creates the Listing Amenities Table
*/

CREATE TABLE listing_amenities (
    listing_id INTEGER NOT NULL REFERENCES listings(id) ON DELETE CASCADE,
    amenity_id INTEGER NOT NULL REFERENCES amenities(id) ON DELETE RESTRICT,
    PRIMARY KEY (listing_id, amenity_id)
);