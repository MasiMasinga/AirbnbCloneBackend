/*
Author: Masi Masinga
Date: 2026-07-28
Description: This script creates the Support Table
*/

CREATE TABLE support (
    id SERIAL PRIMARY KEY,
    user_id UUID NOT NULL,
    listing_id INTEGER NOT NULL,
    booking_id INTEGER NOT NULL,
    message VARCHAR(100) NOT NULL,
    created_at DATE DEFAULT CURRENT_DATE,
    updated_at TIMESTAMPTZ DEFAULT clock_timestamp() NOT NULL
);