public class Rental
{
    public string Id { get; init; } 

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

        this.Id = GenerateId(rentedItem, rentedTo);
        this.IsActive = true;
    }

    // because GUID wasn't easy to use in this context, we will generate a meaningful ID based on the rental date and the rented item
    // and rental person - this way we can easily identify the rental without needing to look up a GUID
    private string GenerateId(Equipment eq, User user)
    {
        string datePart = RentalDate.ToString("yyyyMMdd");

        string itemPart = eq.Id.ToString();
        string userPart = user.UserName;

        // it should generate something like 20240615-3-jdoe for a rental made on June 15, 2024, for item with ID 3 by user with username jdoe
        return $"{datePart}-{itemPart}-{userPart}";
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
        Console.WriteLine($"Rental ID: {Id}");
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