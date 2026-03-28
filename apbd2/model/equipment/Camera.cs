class Camera : Equipment
{
    public string MaxResolution { get; set; } // in format "Width x Height" (e.g., "6000x4000")
    public string CameraType { get; set; } // Professional, Semipro, Amateur

    public Camera(string name, string maxResolution, string cameraType, double boughtPrice, double rentalPrice, DateOnly boughtDate)
        : base(name, boughtPrice, rentalPrice, boughtDate)
    {
        MaxResolution = maxResolution;
        CameraType = cameraType;
    }

}