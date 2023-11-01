-- Create the tables for products, prices, and stocks
CREATE SCHEMA IF NOT EXISTS dbo;

CREATE TABLE dbo.product (
    Id SERIAL PRIMARY KEY,
    Name TEXT,
    Brand TEXT,
    Size TEXT
);

CREATE TABLE dbo.price (
    ProductId INT PRIMARY KEY,
    Amount DECIMAL
);

CREATE TABLE dbo.stock (
    ProductId INT PRIMARY KEY,
    Amount INT
);
