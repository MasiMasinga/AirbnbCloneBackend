/*
Author: Masi Masinga
Date: 2026-08-05
Description: This script creates the Listing Photos Table
*/

CREATE TABLE listing_photos (
    id SERIAL PRIMARY KEY,
    listing_id INTEGER NOT NULL REFERENCES listings(id) ON DELETE CASCADE,
    url TEXT NOT NULL,
    sort_order INTEGER DEFAULT 0
);