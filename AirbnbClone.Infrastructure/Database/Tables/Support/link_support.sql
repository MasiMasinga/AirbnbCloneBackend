
ALTER TABLE support
    ADD CONSTRAINT fk_support_user
        FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE;

ALTER TABLE support
    ADD CONSTRAINT fk_support_listing
        FOREIGN KEY (listing_id) REFERENCES listings(id) ON DELETE SET NULL;

ALTER TABLE support
    ADD CONSTRAINT fk_support_booking
        FOREIGN KEY (booking_id) REFERENCES booking(id) ON DELETE SET NULL;

ALTER TABLE support
    ALTER COLUMN listing_id DROP NOT NULL;

ALTER TABLE support
    ALTER COLUMN booking_id DROP NOT NULL;