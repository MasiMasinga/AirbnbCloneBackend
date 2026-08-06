/*
Author: Masi Masinga
Date: 2026-07-28
Description: This script creates the Listings Table
*/

CREATE TABLE listings (
    id SERIAL PRIMARY KEY,
    title VARCHAR(100) NOT NULL,
    description VARCHAR(140) NOT NULL,
    house_rules VARCHAR(100) NOT NULL,
    pricing NUMERIC(12, 2) DEFAULT 0.00 NOT NULL,
    availability BOOLEAN NULL,
    bed_count INTEGER NOT NULL,
    bath_count INTEGER NOT NULL,
    property_type VARCHAR(20) NOT NULL,
    address VARCHAR(100) NOT NULL,
    location_id INTEGER NOT NULL,
    user_id UUID NOT NULL,
    created_at DATE DEFAULT CURRENT_DATE,
    updated_at TIMESTAMPTZ DEFAULT clock_timestamp() NOT NULL
);
