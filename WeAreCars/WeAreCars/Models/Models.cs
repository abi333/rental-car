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
        public bool SafetyPackage { get; set; }
        public bool PremiumInsurance { get; set; }

        public decimal TotalCost { get; set; }
        public int StaffID { get; set; }
        public DateTime BookingDate { get; set; }

        /// <summary>
        /// Calculates total cost using tiered pricing based on rental duration and dynamic modifiers.
        /// Tier 1 (1-7 days): Base rate, Tier 2 (8-14 days): 10% discount, Tier 3 (15+ days): 20% discount
        /// Car type multipliers, fuel surcharges, and optional add-ons are applied.
        /// </summary>
        /// <returns>The final cost calculated in GBP (£).</returns>
        public decimal CalculateTotalCost()
        {
            // Tiered base pricing based on duration
            decimal dailyRate = 25m;
            decimal baseSubtotal;

            if (NumberOfDays <= 7)
            {
                baseSubtotal = NumberOfDays * dailyRate;
            }
            else if (NumberOfDays <= 14)
            {
                baseSubtotal = NumberOfDays * dailyRate * 0.90m; // 10% discount
            }
            else
            {
                baseSubtotal = NumberOfDays * dailyRate * 0.80m; // 20% discount
            }

            // Apply car type multipliers (percentage-based)
            decimal carMultiplier = 1.0m;
            switch (SelectedCarType)
            {
                case CarType.FamilyCar:
                    carMultiplier = 1.15m; // 15% increase
                    break;
                case CarType.SportsCar:
                    carMultiplier = 1.30m; // 30% increase
                    break;
                case CarType.SUV:
                    carMultiplier = 1.20m; // 20% increase
                    break;
            }

            decimal costAfterCarType = baseSubtotal * carMultiplier;

            // Fuel type surcharges (fixed amounts per day)
            decimal fuelSurcharge = 0m;
            switch (SelectedFuelType)
            {
                case FuelType.Hybrid:
                    fuelSurcharge = 5m * NumberOfDays;
                    break;
                case FuelType.FullElectric:
                    fuelSurcharge = 8m * NumberOfDays;
                    break;
            }

            decimal costWithFuel = costAfterCarType + fuelSurcharge;

            // Optional add-ons (tiered pricing)
            decimal addOnsCost = 0m;
            if (UnlimitedMileage)
            {
                if (NumberOfDays <= 7)
                    addOnsCost += 8m * NumberOfDays;
                else if (NumberOfDays <= 14)
                    addOnsCost += 6m * NumberOfDays;
                else
                    addOnsCost += 4m * NumberOfDays;
            }

            if (BreakdownCover)
            {
                addOnsCost += 3m * NumberOfDays;
            }

            if (SafetyPackage)
            {
                addOnsCost += 5m * NumberOfDays;
            }

            if (PremiumInsurance)
            {
                decimal insuranceCost = costWithFuel * 0.12m; // 12% of subtotal
                addOnsCost += insuranceCost;
            }

            decimal totalCost = costWithFuel + addOnsCost;
            return Math.Round(totalCost, 2);
        }
    }
}
