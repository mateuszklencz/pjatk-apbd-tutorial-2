class Projector : Equipment
{
    public int BrightnessLumen { get; set; }
    public bool IsBluetooth { get; set; }

    public Projector(string name, int brightnessLumen, bool isBluetooth, double boughtPrice, double rentalPrice, DateOnly boughtDate)
        : base(name, boughtPrice, rentalPrice, boughtDate)
    {
        BrightnessLumen = brightnessLumen;
        IsBluetooth = isBluetooth;
    }

}