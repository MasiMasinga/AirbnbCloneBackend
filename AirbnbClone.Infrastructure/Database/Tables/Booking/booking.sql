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
    listing_id INTEGER NOT NULL,
);


ALTER TABLE booking
    ADD CONSTRAINT fk_booking_user
        FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE;

ALTER TABLE booking
    ADD COLUMN listing_id INTEGER;

ALTER TABLE booking
    ADD CONSTRAINT fk_booking_listing
        FOREIGN KEY (listing_id) REFERENCES listings(id) ON DELETE RESTRICT;
 

ALTER TABLE booking
    ADD COLUMN created_at DATE DEFAULT CURRENT_DATE;

ALTER TABLE booking
    ADD COLUMN updated_at TIMESTAMPTZ DEFAULT clock_timestamp() NOT NULL;