/*
Author: Masi Masinga
Date: 2026-07-28
Description: This script creates the User Table
*/

CREATE TABLE user (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_role VARCHAR(20) NOT NULL,
    title VARCHAR(100) NOT NULL,
    first_name VARCHAR(140) NOT NULL,
    surname VARCHAR(140) NOT NULL,
    bio VARCHAR(100) NOT NULL,
    photo TEXT NOT NULL,
    created_at DATE DEFAULT CURRENT_DATE,
    updated_at TIMESTAMPTZ DEFAULT clock_timestamp() NOT NULL
);