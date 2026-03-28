using System;
using System.Collections.Generic;

public static class EquipmentRepo
{
    static private Dictionary<int, Equipment> _equipment = new();

    public static void addEquipment(Equipment equipment)
    {
        _equipment[equipment.Id] = equipment;
    }

    public static Equipment? getEquipment(int id)
    {
        return _equipment.ContainsKey(id) ? _equipment[id] : null;
    }

    public static List<Equipment> getAllEquipment()
    {
        return new List<Equipment>(_equipment.Values);
    }

    public static void displayAllEquipment()
    {
        Console.WriteLine("=== All Equipment ===");
        foreach (var equipment in _equipment.Values)
        {
            equipment.DisplayInfo();
            Console.WriteLine();
        }
    }

    public static void createEquipmentEntry(
        string equipmentType,
        string name,
        double boughtPrice,
        double rentalPrice,
        DateOnly boughtDate,
        // optional type-specific parameters - allow nullable to avoid nullability warnings
        int ram = 0,
        int storage = 0,
        string? maxResolution = null,
        string? cameraType = null,
        int brightnessLumen = 0,
        bool isBluetooth = false)
    {
        Equipment newEquipment;

        if (boughtPrice < 0 || rentalPrice < 0)
        {
            Console.WriteLine("Cannot create equipment: prices must be non-negative.");
            return;
        }

        switch (equipmentType)
        {
            case "Laptop":
                newEquipment = new Laptop(name, ram, storage, boughtPrice, rentalPrice, boughtDate);
                break;

            case "Camera":
                newEquipment = new Camera(
                    name,
                    maxResolution ?? "Unknown",
                    cameraType ?? "Unknown",
                    boughtPrice,
                    rentalPrice,
                    boughtDate);
                break;

            case "Projector":
                newEquipment = new Projector(name, brightnessLumen, isBluetooth, boughtPrice, rentalPrice, boughtDate);
                break;

            default:
                Console.WriteLine($"Unknown equipment type: {equipmentType}. Defaulting to Laptop.");
                newEquipment = new Laptop(name, ram, storage, boughtPrice, rentalPrice, boughtDate);
                break;
        }

        addEquipment(newEquipment);
    }
}