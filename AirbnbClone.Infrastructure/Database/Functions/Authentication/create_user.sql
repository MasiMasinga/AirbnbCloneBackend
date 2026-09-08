CREATE OR REPLACE FUNCTION create_user(
    p_user_role VARCHAR(20),
    p_title VARCHAR(100),
    p_first_name VARCHAR(140),
    p_email_address VARCHAR(140),
    p_surname VARCHAR(140),
    p_bio VARCHAR(100),
    p_photo TEXT,
    p_password_hash TEXT
)
RETURNS UUID
LANGUAGE plpgsql
AS $$
DECLARE
new_user_id UUID;
BEGIN
INSERT INTO users (
    user_role,
    title,
    first_name,
    email_address,
    surname,
    bio,
    photo,
    password_hash
)
VALUES (
           p_user_role,
           p_title,
           p_first_name,
           p_email_address,
           p_surname,
           p_bio,
           p_photo,
           p_password_hash
       )
    RETURNING id INTO new_user_id;

RETURN new_user_id;
END;
$$;