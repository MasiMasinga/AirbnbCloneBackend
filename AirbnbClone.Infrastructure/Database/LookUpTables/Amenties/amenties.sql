/*
Author: Masi Masinga
Date: 2026-08-05
Description: This script creates the Amenities Table
*/

CREATE TABLE amenities (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) UNIQUE NOT NULL
);