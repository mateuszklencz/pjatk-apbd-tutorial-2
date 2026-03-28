public class Rental
{
    public Guid InstanceId { get; } = Guid.NewGuid(); // https://learn.microsoft.com/pl-pl/dotnet/api/system.guid.newguid?view=net-9.0
                                                      // value will be something like e150e1ec-5285-49f0-96c5-d78a4904555c

    public DateOnly RentalDate { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly? ActualReturn { get; set; }
    public bool IsActive { get; set; }

    public User RentedTo { get; set; }
    public Equipment RentedItem { get; set; }

    public Rental(DateOnly rentalDate, DateOnly dueDate, User rentedTo, Equipment rentedItem, DateOnly? actualReturn = null)
    {
        RentalDate = rentalDate;
        DueDate = dueDate;
        RentedTo = rentedTo;
        RentedItem = rentedItem;
        ActualReturn = actualReturn;
    }

    public double CalculateRentalFee()
    {
        int rentalDays = DueDate.DayNumber - RentalDate.DayNumber;
        return rentalDays * RentedItem.RentalPricePerDay;
    }

    
    public double CalculatePenalty()
    {
        if (ActualReturn.HasValue && ActualReturn.Value > DueDate)
        {
            int lateDays = ActualReturn.Value.DayNumber - DueDate.DayNumber;
            return lateDays * RentedItem.BoughtPrice * 0.01;
        }
        return 0;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Rental ID: {InstanceId}");
        Console.WriteLine($"Rental Date: {RentalDate}");
        Console.WriteLine($"Due Date:     {DueDate}");
        Console.WriteLine($"Actual Return:{(ActualReturn.HasValue ? ActualReturn.Value.ToString() : "N/A")}");
        Console.WriteLine();

        Console.Write("Rented To: ");
        if (RentedTo != null)
        {
            Console.WriteLine(RentedTo.ToString());
        }
        else
        {
            Console.WriteLine("  <no user assigned>");
        }
        Console.WriteLine();

        Console.Write("Rented Item: ");
        if (RentedItem != null)
        {
            Console.WriteLine(RentedItem.ToString());
        }
        else
        {
            Console.WriteLine("  <no item assigned>");
        }
        Console.WriteLine();

        Console.WriteLine($"Estimated Rental Fee: {CalculateRentalFee()} PLN");
        Console.WriteLine($"Estimated Penalty:    {CalculatePenalty()} PLN");
    }
}