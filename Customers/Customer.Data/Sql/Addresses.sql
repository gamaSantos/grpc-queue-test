CREATE TABLE Addresses(
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    CustomerId UNIQUEIDENTIFIER,
    StreetName varchar(500) not null,
    StreetNumber varchar(10) not null,
    Observation VARCHAR(2000),
    CONSTRAINT Addresses_Customers_FK FOREIGN KEY (CustomerId) REFERENCES Customers (Id)
)