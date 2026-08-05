
ALTER TABLE booking
    ADD CONSTRAINT fk_booking_user
        FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE;

ALTER TABLE booking
    ADD COLUMN listing_id INTEGER;

ALTER TABLE booking
    ADD CONSTRAINT fk_booking_listing
        FOREIGN KEY (listing_id) REFERENCES listings(id) ON DELETE RESTRICT;
 