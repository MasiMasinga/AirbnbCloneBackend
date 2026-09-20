/*
    Author: Masi Masinga
    Date: 2026-09-20
    Description: This script updates a entry from the Users Table
*/

CREATE OR REPLACE FUNCTION fn_update_user_details(
    p_id UUID,
    p_user_role VARCHAR,
    p_title VARCHAR,
    p_first_name VARCHAR,
    p_surname VARCHAR,
    p_email_address VARCHAR,
    p_bio TEXT,
    p_photo TEXT,
    p_password TEXT
)
    RETURNS BOOLEAN
    LANGUAGE sql
AS
$$
WITH updated AS (
    UPDATE users
        SET
            user_role = COALESCE(p_user_role, user_role),
            title = COALESCE(p_title, title),
            first_name = COALESCE(p_first_name, first_name),
            surname = COALESCE(p_surname, surname),
            email_address = COALESCE(p_email_address, email_address),
            bio = COALESCE(p_bio, bio),
            photo = COALESCE(p_photo, photo),
            password_hash = COALESCE(p_password, password_hash),
            updated_at = clock_timestamp()
        WHERE id = p_id
        RETURNING id
)
SELECT EXISTS (SELECT 1 FROM updated);
$$;
