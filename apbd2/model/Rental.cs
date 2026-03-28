public class Rental
{
    public DateOnly RentalDate { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly? ActualReturn { get; set; }

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
        int rentalDays = (DueDate - RentalDate).Days;
        return rentalDays * RentedItem.RentalPricePerDay;
    }

    public double CalculatePenalty()
    {
        if (ActualReturn > DueDate)
        {
            int lateDays = (ActualReturn.Value - DueDate).Days;
            return lateDays * RentedItem.BoughtPrice * 0.01;
        }
        return 0;
    }
}