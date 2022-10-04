CREATE TABLE Customers(
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    FirstName varchar(200) not null,
    LastName varchar(200) not null,
    PhoneNumber VARCHAR(20) not null,
    PhoneRegion VARCHAR(3) not null
)