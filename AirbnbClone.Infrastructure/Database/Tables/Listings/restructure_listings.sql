ALTER TABLE listings
    ADD COLUMN location_id INTEGER;

ALTER TABLE listings
    ALTER COLUMN location_id SET NOT NULL;

ALTER TABLE listings
    ADD CONSTRAINT fk_listings_location
        FOREIGN KEY (location_id) REFERENCES locations(id) ON DELETE RESTRICT;

ALTER TABLE listings
DROP COLUMN amenities,
    DROP COLUMN photo,
    DROP COLUMN location,
    DROP COLUMN country,
    DROP COLUMN start_date,
    DROP COLUMN end_date;
 