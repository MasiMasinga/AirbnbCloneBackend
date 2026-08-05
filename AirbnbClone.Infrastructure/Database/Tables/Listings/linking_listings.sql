ALTER TABLE listings
    ADD CONSTRAINT fk_listings_user
        FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE;

ALTER TABLE listings
DROP COLUMN review_id;