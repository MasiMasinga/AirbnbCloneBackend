/*
Author: Masi Masinga
Date: 2026-07-28
Description: This script creates the Booking Table
*/

CREATE TABLE booking (
    id SERIAL PRIMARY KEY,
    user_id UUID NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    amount NUMERIC(12, 2) DEFAULT 0.00 NOT NULL,
    number_of_guests INTEGER NOT NULL,
);