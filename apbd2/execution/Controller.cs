using System;
using System.Collections.Generic;
using System.Linq;

public static class Controller
{
    // Business rule constants - centralized and easy to modify
    private const int MAX_STUDENT_RENTALS = 2;
    private const int MAX_EMPLOYEE_RENTALS = 5;

    // 1. Add a new user to the system
    public static void AddUser(string userType, string firstName, string lastName)
    {
        try
        {
            UserRepo.createUserEntry(userType, "", firstName, lastName);
            Console.WriteLine($"User {firstName} {lastName} added successfully as {userType}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add user: {ex.Message}");
        }
    }

    // 2. Add a new equipment item of a selected type
    public static void AddEquipment(
        string equipmentType,
        string name,
        double boughtPrice,
        double rentalPrice,
        DateOnly boughtDate,
        int ram = 0,
        int storage = 0,
        string? maxResolution = null,
        string? cameraType = null,
        int brightnessLumen = 0,
        bool isBluetooth = false)
    {
        EquipmentRepo.createEquipmentEntry(
            equipmentType,
            name,
            boughtPrice,
            rentalPrice,
            boughtDate,
            ram,
            storage,
            maxResolution,
            cameraType,
            brightnessLumen,
            isBluetooth);
    }

    // 3. Display the full list of equipment together with current status
    public static void DisplayAllEquipment()
    {
        var equipment = EquipmentRepo.getAllEquipment();
        if (equipment.Count == 0)
        {
            Console.WriteLine("No equipment in the system.");
            return;
        }

        Console.WriteLine("===========================================");
        Console.WriteLine("    ALL EQUIPMENT (FULL LIST)");
        Console.WriteLine("===========================================");
        foreach (var item in equipment)
        {
            item.DisplayInfo();
            Console.WriteLine($"Available: {item.IsAvailable}, Rented: {item.IsRented}");
            Console.WriteLine();
        }
    }

    // 4. Display only equipment currently available for rental
    public static void DisplayAvailableEquipment()
    {
        var allEquipment = EquipmentRepo.getAllEquipment();
        var availableEquipment = allEquipment.Where(e => e.IsAvailable && !e.IsRented).ToList();

        if (availableEquipment.Count == 0)
        {
            Console.WriteLine("No equipment currently available for rental.");
            return;
        }

        Console.WriteLine("===========================================");
        Console.WriteLine("    AVAILABLE EQUIPMENT FOR RENTAL");
        Console.WriteLine("===========================================");
        foreach (var item in availableEquipment)
        {
            item.DisplayInfo();
            Console.WriteLine();
        }
        Console.WriteLine($"Total available: {availableEquipment.Count}");
    }

    // 5. Rent equipment to a user (provide startDate and endDate) - use RentalRepo to create entry
    public static bool RentEquipment(string userName, int equipmentId, DateOnly startDate, DateOnly endDate)
    {
        if (endDate < startDate) return false;

        var user = UserRepo.getUser(userName);
        if (user == null) return false;

        var equipment = EquipmentRepo.getEquipment(equipmentId);
        if (equipment == null) return false;

        if (!equipment.IsAvailable || equipment.IsRented) return false;

        int activeRentalsCount = GetActiveRentalsCountForUser(userName);
        int maxRentals = GetMaxRentalsForUser(user);
        if (activeRentalsCount >= maxRentals) return false;

        // mark equipment as rented and let the repo create and store the rental
        equipment.MarkAsRented();
        RentalRepo.createRentalEntry(startDate, endDate, user, equipment);

        return true;
    }

    // 6. Return equipment and calculate a possible late penalty (uses RentalRepo.terminateRental)
    public static bool ReturnEquipment(string rentalId, DateOnly returnDate)
    {
        var rental = RentalRepo.getRental(rentalId);
        if (rental == null || !rental.IsActive) return false;

        // terminate in repo (sets ActualReturn and IsActive)
        RentalRepo.terminateRental(rentalId, returnDate);

        // mark item as returned
        rental.RentedItem.MarkAsReturned();

        // calculate and print fees (minimal)
        double rentalFee = rental.CalculateRentalFee();
        double penalty = rental.CalculatePenalty();

        Console.WriteLine($"Rental Fee: {rentalFee} PLN");
        Console.WriteLine($"Penalty: {penalty} PLN");
        Console.WriteLine($"Total: {(rentalFee + penalty)} PLN");

        return true;
    }


    // 7. Mark equipment as unavailable
    public static bool MarkEquipmentUnavailable(int equipmentId, string reason = "maintenance or damage")
    {
        var equipment = EquipmentRepo.getEquipment(equipmentId);
        if (equipment == null || equipment.IsRented) return false;

        equipment.MarkAsUnavailable();
        return true;
    }

    // 8. Display active rentals for a selected user
    public static void DisplayActiveRentalsForUser(string userName)
    {
        var user = UserRepo.getUser(userName);

        var allRentals = RentalRepo.getAllRentals();
        var activeRentals = allRentals.Where(r => r.RentedTo.UserName == userName && r.IsActive).ToList();

        foreach (var rental in activeRentals)
        {
            rental.DisplayInfo();
            Console.WriteLine();
        }
    }

    // 9. Display the list of overdue rentals
    public static void DisplayOverdueRentals()
    {
        var allRentals = RentalRepo.getAllRentals();
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        var overdueRentals = allRentals
            .Where(r => r.IsActive && r.DueDate < today)
            .OrderBy(r => r.DueDate)
            .ToList();

        foreach (var rental in overdueRentals)
        {
            int daysOverdue = today.DayNumber - rental.DueDate.DayNumber;
            Console.WriteLine($"Rental ID: {rental.Id}");
            Console.WriteLine($"Equipment: {rental.RentedItem.Name}");
            Console.WriteLine($"User: {rental.RentedTo.UserName}");
            Console.WriteLine($"Days Overdue: {daysOverdue}");
            Console.WriteLine();
        }
    }

    // 10. Generate a short summary report of the rental service state
    public static void GenerateSummaryReport()
    {
        var allEquipment = EquipmentRepo.getAllEquipment();
        var allUsers = UserRepo.getAllUsers();
        var allRentals = RentalRepo.getAllRentals();

        Console.WriteLine("=== Equipment Information ===\n");
        foreach (var equipment in allEquipment)
        {
            equipment.DisplayInfo();
            Console.WriteLine();
        }

        Console.WriteLine("=== User Information ===\n");
        foreach (var user in allUsers)
        {
            user.DisplayInfo();
            Console.WriteLine();
        }

        Console.WriteLine("=== Rentals Information ===\n");
        foreach (var rental in allRentals)
        {
            rental.DisplayInfo();
            Console.WriteLine();
        }
    }

    private static int GetActiveRentalsCountForUser(string userName)
    {
        var allRentals = RentalRepo.getAllRentals();
        return allRentals.Count(r => r.RentedTo.UserName == userName && r.IsActive);
    }

    private static int GetMaxRentalsForUser(User user)
    {
        if (user is Student) return MAX_STUDENT_RENTALS;
        if (user is Employee) return MAX_EMPLOYEE_RENTALS;
        return MAX_STUDENT_RENTALS;
    }
}