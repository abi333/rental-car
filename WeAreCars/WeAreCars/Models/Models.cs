using System;

namespace WeAreCars.Models
{
    /// <summary>
    /// Represents the different types of cars available for rent.
    /// </summary>
    public enum CarType
    {
        CityCar,
        FamilyCar,
        SportsCar,
        SUV
    }

    /// <summary>
    /// Represents the different fuel types available for the cars.
    /// </summary>
    public enum FuelType
    {
        Petrol,
        Diesel,
        Hybrid,
        FullElectric
    }

    /// <summary>
    /// Represents a system user or staff member.
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents a single car booking transaction.
    /// </summary>
    public class CarBooking
    {
        public int Id { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerAddress { get; set; }
        public int CustomerAge { get; set; }
        public bool HasValidDrivingLicense { get; set; }
        public int NumberOfDays { get; set; } // 1 to 28
        public CarType SelectedCarType { get; set; }
        public FuelType SelectedFuelType { get; set; }
        public bool UnlimitedMileage { get; set; }
        public bool BreakdownCover { get; set; }

        public decimal TotalCost { get; set; }
        public int StaffID { get; set; }
        public DateTime BookingDate { get; set; }

        /// <summary>
        /// Analyzes the current booking configurations (days, car type, extra covers) 
        /// and applies required mathematics to return a final decimal cost.
        /// </summary>
        /// <returns>The final cost calculated in GBP (£).</returns>
        public decimal CalculateTotalCost()
        {
            decimal baseRate = 25m;
            decimal totalCost = NumberOfDays * baseRate;

            switch (SelectedCarType)
            {
                case CarType.FamilyCar:
                    totalCost += 50m;
                    break;
                case CarType.SportsCar:
                    totalCost += 75m;
                    break;
                case CarType.SUV:
                    totalCost += 65m;
                    break;
            }

            switch (SelectedFuelType)
            {
                case FuelType.Hybrid:
                    totalCost += 30m;
                    break;
                case FuelType.FullElectric:
                    totalCost += 50m;
                    break;
            }

            if (UnlimitedMileage) totalCost += 10m * NumberOfDays;
            if (BreakdownCover) totalCost += 2m * NumberOfDays;

            return totalCost;
        }
    }
}