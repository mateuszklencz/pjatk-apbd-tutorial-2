public abstract class Equipment
{
    static int _idCounter = 0; // Static counter to generate unique IDs

    public int Id { get; private set; }
    public string Name { get; private set; }
    public bool IsAvailable { get; private set; } // for damaged and maintenance status
    public bool IsRented { get; private set; } // for rental status

    // basic shared data - exposed as read-only properties
    public double BoughtPrice { get; private set; }
    public double RentalPricePerDay { get; private set; }
    public DateOnly BoughtDate { get; private set; }

    // constructor
    protected Equipment(string name, double boughtPrice, double rentalPrice, DateOnly boughtDate)
    {
        this.Id = ++_idCounter;
        this.Name = name;
        this.BoughtPrice = boughtPrice;
        this.RentalPricePerDay = rentalPrice;
        this.BoughtDate = boughtDate;
        this.IsAvailable = true;
        this.IsRented = false;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Bought Price: {BoughtPrice} PLN");
        Console.WriteLine($"Rental Price per Day: {RentalPricePerDay} PLN");
        Console.WriteLine($"Bought Date: {BoughtDate}");
        Console.WriteLine($"Availability: {(IsAvailable ? "Available" : "Not Available")}");
    }

    public override string ToString()
    {
        return $"{Id}: {GetType().Name} - ({Name})";
    }   

    // If an equipment item is marked as unavailable, it cannot be rented
    public void MarkAsRented()
    {
        if (IsAvailable)
        {
            IsRented = true;
            IsAvailable = false;
        }
        else
        {
            Console.WriteLine("Cannot rent this equipment. It is not available.");
        }
    }

    public void MarkAsReturned()
    {
        if (IsRented)
        {
            IsRented = false;
            IsAvailable = true; // assuming it's returned in good condition
        }
        else
        {
            Console.WriteLine("This equipment is not currently rented.");
        }
    }

    public void MarkAsUnavailable() // for damage or maintenance
    {
        IsAvailable = false;
    }

    public void MarkAsAvailable() 
    {
        if (!IsRented) // only mark as available if it's not currently rented
        {
            IsAvailable = true;
        }
        else
        {
            Console.WriteLine("Cannot mark as available while it is rented.");
        }
    }

    // logic is that when equipment is returned, it becomes available again (unless it's damaged)

}