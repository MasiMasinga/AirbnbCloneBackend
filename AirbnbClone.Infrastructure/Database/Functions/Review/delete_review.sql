CREATE OR REPLACE FUNCTION fn_review_delete(
    review_id INT
)
RETURNS INT
LANGUAGE sql
AS $$
    DELETE FROM review
    WHERE id = review_id
    RETURNING id;
$$;