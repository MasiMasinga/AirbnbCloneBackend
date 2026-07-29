/*
Author: Masi Masinga
Date: 2026-07-29
Description: This script deletes a entry from the Listings Table
*/

CREATE OR REPLACE FUNCTION delete_listing
(
    listing_id INTEGER
)
       RETURNS VOID
       LANGUAGE plpgsql
AS
$$
BEGIN

DELETE
FROM listings
WHERE id = listing_id;

END;
$$;