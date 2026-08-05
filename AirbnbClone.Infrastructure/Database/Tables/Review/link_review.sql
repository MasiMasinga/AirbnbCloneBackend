ALTER TABLE review
    ADD CONSTRAINT fk_review_user
        FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE;

ALTER TABLE review
    ADD CONSTRAINT fk_review_listing
        FOREIGN KEY (listing_id) REFERENCES listings(id) ON DELETE CASCADE;

ALTER TABLE review
    ADD CONSTRAINT fk_review_booking
        FOREIGN KEY (booking_id) REFERENCES booking(id) ON DELETE CASCADE;

ALTER TABLE review
    RENAME COLUMN location TO location_rating;