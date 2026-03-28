class Laptop : Equipment
{
    public int RAM { get; set; } // in GB
    public int Storage { get; set; } // in GB

    public Laptop(string name, int ram, int storage, double boughtPrice, double rentalPrice, DateOnly boughtDate)
        : base(name, boughtPrice, rentalPrice, boughtDate)
    {
        RAM = ram;
        Storage = storage;
    }

}