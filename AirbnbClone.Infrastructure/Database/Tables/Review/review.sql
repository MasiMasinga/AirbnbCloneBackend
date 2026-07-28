/*
Author: Masi Masinga
Date: 2026-07-28
Description: This script creates the Review Table
*/

CREATE TABLE review (
    id SERIAL PRIMARY KEY,
    user_id UUID NOT NULL,
    listing_id INTEGER NOT NULL,
    booking_id INTEGER NOT NULL,
    comment VARCHAR(100) NOT NULL,
    rating INTEGER NOT NULL,
    cleanliness INTEGER NOT NULL,
    accuracy INTEGER NOT NULL,
    check_in INTEGER NOT NULL,
    communication INTEGER NOT NULL,
    location INTEGER NOT NULL,
    value INTEGER NOT NULL,
    overall INTEGER NOT NULL,
    created_at DATE DEFAULT CURRENT_DATE,
    updated_at TIMESTAMPTZ DEFAULT clock_timestamp() NOT NULL
);