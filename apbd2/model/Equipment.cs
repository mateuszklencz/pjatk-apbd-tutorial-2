abstract class Equipment
{
    static int _idCounter = 0; // Static counter to generate unique IDs
    int Id;
    string Name; 
    bool IsAvailable;

    // basic shared data
    double BoughtPrice;
    double RentalPricePerDay;
    DateOnly BoughtDate;

    // constructor
    protected Equipment(string name, double boughtPrice, double rentalPrice, DateOnly boughtDate)
    {
        this.Id = ++_idCounter;
        this.Name = name;
        this.BoughtPrice = boughtPrice;
        this.RentalPrice = rentalPrice;
        this.BoughtDate = boughtDate;
        this.IsAvailable = true;
    }
}