
ALTER TABLE payment
    ADD CONSTRAINT fk_payment_user
        FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE;

ALTER TABLE payment
    ADD COLUMN booking_id INTEGER;

ALTER TABLE payment
    ADD CONSTRAINT fk_payment_booking
        FOREIGN KEY (booking_id) REFERENCES booking(id) ON DELETE RESTRICT;

ALTER TABLE payment
ALTER COLUMN provider_response TYPE JSONB USING NULL;
 