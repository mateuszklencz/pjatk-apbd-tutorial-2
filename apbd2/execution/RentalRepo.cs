using System;
using System.Collections.Generic;
using System.Linq;

public static class RentalRepo
{
    static private Dictionary<string, Rental> _rentals = new();

    public static void addRental(Rental rental)
    {
        // use the InstanceId property from Rental
        _rentals[rental.Id] = rental;
    }

    public static Rental getRental(string guid)
    {
        return _rentals.ContainsKey(guid) ? _rentals[guid] : null;
    }

    public static List<Rental> getAllRentals()
    {
        return _rentals.Values.ToList();
    }

    public static void displayAllRentals()
    {
        Console.WriteLine("=== All Rentals ===");
        foreach (var rental in _rentals.Values)
        {
            rental.DisplayInfo();
            Console.WriteLine();
        }
    }

    public static void createRentalEntry(DateOnly rentalDate, DateOnly dueDate, User rentedTo, Equipment rentedItem)
    {
        Rental newRental = new Rental(rentalDate, dueDate, rentedTo, rentedItem, null);



        addRental(newRental);
    }

    public static void terminateRental(string rentalId, DateOnly actualReturnDate)
    {
        Rental rental = getRental(rentalId);
        if (rental != null && rental.IsActive)
        {
            rental.ActualReturn = actualReturnDate;
            rental.IsActive = false;
        }
    }
}