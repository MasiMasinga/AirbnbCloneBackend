/*
Author: Masi Masinga
Date: 2026-07-28
Description: This script creates the Payment Table
*/

CREATE TABLE payment (
    id SERIAL PRIMARY KEY,
    user_id UUID NOT NULL,
    amount NUMERIC(12, 2) DEFAULT 0.00 NOT NULL,
    status TEXT,
    provider_response NUMERIC(12, 2) DEFAULT 0.00 NOT NULL,
    created_at DATE DEFAULT CURRENT_DATE,
    updated_at TIMESTAMPTZ DEFAULT clock_timestamp() NOT NULL
);