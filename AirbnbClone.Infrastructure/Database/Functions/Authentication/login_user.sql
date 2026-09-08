CREATE OR REPLACE FUNCTION login_user(
    p_email_address VARCHAR(140)
)
RETURNS TABLE (
    id UUID,
    user_role VARCHAR(20),
    title VARCHAR(100),
    first_name VARCHAR(140),
    surname VARCHAR(140),
    email_address VARCHAR(140),
    bio VARCHAR(100),
    photo TEXT,
    password_hash TEXT
)
LANGUAGE plpgsql
AS $$
BEGIN
RETURN QUERY
SELECT
    u.id,
    u.user_role,
    u.title,
    u.first_name,
    u.surname,
    u.email_address,
    u.bio,
    u.photo,
    u.password_hash
FROM users u
WHERE LOWER(u.email_address) = LOWER(p_email_address);
END;
$$;