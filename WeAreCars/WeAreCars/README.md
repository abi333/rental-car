# WeAreCars Management System 🚗

Welcome to the **WeAreCars** Booking Management System. This project demonstrates a beginner-friendly C# Windows Forms application targeting .NET Framework 4.7.2. 

If you are just getting started with C# development, UI construction, or ADO.NET database operations, this reference project breaks down all the basic moving parts that go into creating a functional application! It is built with simplicity in mind.

---

## 📋 Table of Contents
1. Tutorial Overview
2. System Requirements & Features
3. Step 1: Setting up the Database
4. Step 2: Understanding Application Configuration 
5. Step 3: Exploring the Code Architecture
6. Step 4: Running and Troubleshooting

---

## 1. Tutorial Overview
This tutorial intends to bridge the gap between learning syntax and building a functioning piece of software. In this guide, you will learn:
- How C# connects, queries, and writes to a Microsoft SQL database.
- How Object-Oriented principles (Enums and Classes) correlate to physical database rows.
- How event handlers (`Click`, `SelectedIndexChanged`) can create a dynamic user interface experience.

## 2. System Requirements & Features

1. **Authentication**: Users must log in via a fixed credential setup (`sta001` / `givemethekeys123`). The system limits the user to a maximum of 3 invalid attempts before locking them out. 
2. **Booking Management**: Staff members process rentals inside an intuitive UI. Features include standard data fields (name, age, license valid checks) plus dynamically selected rates based on:
   - Base Price: £25/day
   - Upgradable Car Types (Family, Sports, SUV)
   - Alternative Fuel Types (Hybrid, Full Electric)
   - Checkbox Extra Services (Insurance, Unlimited Mileage)
3. **Live Price Estimations**: As users select dropdowns or check boxes, the system visually maps out the mathematical equation leading to the final total cost on the screen, updating in real-time.
4. **Data Persistence (SQL)**: Once a clerk hits "Review & Process" and confirms, the system uses straight ADO.NET variables to ship data directly into a Microsoft SQL Server!

---

## 3. Step 1: Setting up the Database

### SQL Database Requirement
Before running this application, you must create a local SQL server database and schema using the query provided below. Open Microsoft SQL Server Management Studio (SSMS) or your preferred SQL terminal and execute:

```sql
-- Creates our main environment
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
```

---

## 4. Step 2: Understanding Application Configuration 

How does our C# application know where to find the database we just made? It looks in `Data/DatabaseConnection.cs`!

If you open the `DatabaseConnection.cs` file, you will notice a static connection string.

```csharp
private static string connectionString = @"Server=localhost\SQLEXPRESS;Database=WeAreCars;Integrated Security=True;";
```
To understand this:
- **`Server=localhost\SQLEXPRESS`**: Tells our application to look on the *current* machine for a SQL Express instance. 
- **`Database=WeAreCars`**: Focuses C# permanently on the exact database we just created.
- **`Integrated Security=True`**: States that C# will leverage your Windows account to bypass SQL password requests securely.

Whenever our application needs to make a query, it dynamically requests a connection object utilizing this strict central configuration string.

---

## 5. Step 3: Exploring the Code Architecture

The project has intentionally been set up cleanly with three main aspects:

### A) The Blueprint: `Models/Models.cs`
This file contains pure "Property" classes like `CarBooking`. Think of this as the "blueprint". 
- Data isn't randomly stored around the form. When a user fills out boxes, we construct a new `CarBooking` object and attach those UI values firmly to the blueprint.
- This file also holds intelligent business logic natively. For example, `CalculateTotalCost()` is housed *inside* the Blueprint, meaning the object itself calculates its mathematical total securely.

### B) The Gateway: `Data/DatabaseConnection.cs`
Responsible purely for maintaining the SQL Connection string internally and manufacturing a ready-to-fire standard SQL `SqlConnection` out of it. 

### C) The Interface: `Form1.cs`
This is your Windows form containing all the logic! Dive inside:
- **`SetupUI()`:** Rather than dragging and dropping in a designer, this code creates text boxes, drop-downs, and labels manually. 
- **`BtnLogin_Click()`:** Validates login by directly checking User identities persisted dynamically on the database server.
- **`UpdatePriceEstimate()`:** A cornerstone example of real-time UI mapping. This function is attached to multiple checkboxes. If users change their mind, it fires mathematically and displays the work on the live UI.
- **`UpdateBookingsList()`:** Uses ADO.NET SQL Data Readers to map live historical elements out of the server directly onto visual rows.
- **`BtnBook_Click()` & `SaveBookingToDatabase()`:** Represents robust, secure data entry. It demonstrates scalable `SqlCommand` statements leveraging exactly SQL "Parameters" protecting from SQL injections.

---

## 6. Step 4: Running and Troubleshooting

1. Ensure the Database script from Step 1 has correctly executed.
2. Open Microsoft Visual Studio Community.
3. Highlight the `WeAreCars` project in Solution Explorer.
4. Press **F5** (Or `Debug > Start Debugging`).

**Common Troubleshooting:**
- **App Crashes on Database Operations**: Carefully check your `DatabaseConnection.cs` file. If your local SQL server instance uses a different name than `localhost\SQLEXPRESS` (such as simply `(local)` or `localhost`), you must update it to match.
- **Account Locked immediately**: Login with the system default of Username: `sta001`, Password: `givemethekeys123`.

Enjoy exploring C# & .NET Forms!