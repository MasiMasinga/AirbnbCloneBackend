CREATE OR REPLACE FUNCTION check_existing_user(
    p_email_address VARCHAR(140)
)
RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
BEGIN
RETURN EXISTS (
    SELECT 1
    FROM users
    WHERE email_address = p_email_address
);
END;
$$;