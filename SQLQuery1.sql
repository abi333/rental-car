CREATE DATABASE WeAreCars;
GO
USE WeAreCars;
GO

-- Creates our Users table and inserts a default staff account
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL
);
INSERT INTO Users (Username, Password) VALUES ('sta001', 'givemethekeys123');

-- Creates our booking environment representing the C# Models structure
CREATE TABLE Car_Booking (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerFirstName NVARCHAR(100) NOT NULL,
    CustomerSurname NVARCHAR(100) NOT NULL,
    CustomerAddress NVARCHAR(500) NOT NULL,
    CustomerAge INT NOT NULL,
    HasValidDrivingLicense BIT NOT NULL,
    NumberOfDays INT NOT NULL CHECK (NumberOfDays >= 1 AND NumberOfDays <= 28),
    CarType INT NOT NULL,    
    FuelType INT NOT NULL,   
    HasUnlimitedMileage BIT NOT NULL DEFAULT 0,
    HasBreakdownCover BIT NOT NULL DEFAULT 0,
    TotalCost DECIMAL(18,2) NOT NULL,
    StaffID INT NOT NULL,
    BookingDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_CarBooking_Users FOREIGN KEY (StaffID) REFERENCES Users(UserID)
);