using System;
using System.Collections.Generic;
using System.Linq;

public static class EquipmentRepo
{
    static private Dictionary<int, Equipment> _equipment = new();

    public static void addEquipment(Equipment equipment)
    {
        _equipment[equipment.Id] = equipment;
    }

    public static Equipment getEquipment(int id)
    {
        return _equipment.ContainsKey(id) ? _equipment[id] : null;
    }

    public static List<Equipment> getAllEquipment()
    {
        return _equipment.Values.ToList();
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
}