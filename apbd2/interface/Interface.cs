using System;

namespace apbd2
{
    public static class Interface
    {
        public static void RunInteractiveMenu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║               UNIVERSITY EQUIPMENT RENTAL SERVICE             ║");
                Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine("  1.  Add User");
                Console.WriteLine("  2.  Add Equipment");
                Console.WriteLine("  3.  Display All Equipment");
                Console.WriteLine("  4.  Display Available Equipment");
                Console.WriteLine("  5.  Rent Equipment");
                Console.WriteLine("  6.  Return Equipment");
                Console.WriteLine("  7.  Mark Equipment as Unavailable");
                Console.WriteLine("  8.  Display Active Rentals for User");
                Console.WriteLine("  9.  Display Overdue Rentals");
                Console.WriteLine("  10. Generate Summary Report");
                Console.WriteLine("  11. Run Full Demonstration");
                Console.WriteLine("  0.  Exit");
                Console.WriteLine();
                Console.Write("Select an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddUserMenu();
                        break;
                    case "2":
                        AddEquipmentMenu();
                        break;
                    case "3":
                        Controller.DisplayAllEquipment();
                        PauseForUser();
                        break;
                    case "4":
                        Controller.DisplayAvailableEquipment();
                        PauseForUser();
                        break;
                    case "5":
                        RentEquipmentMenu();
                        break;
                    case "6":
                        ReturnEquipmentMenu();
                        break;
                    case "7":
                        MarkUnavailableMenu();
                        break;
                    case "8":
                        DisplayActiveRentalsMenu();
                        break;
                    case "9":
                        Controller.DisplayOverdueRentals();
                        PauseForUser();
                        break;
                    case "10":
                        Controller.GenerateSummaryReport();
                        PauseForUser();
                        break;
                    case "11":
                        Console.Clear();
                        InterfaceDemonstration.RunDemonstration();
                        PauseForUser();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("\nThank you for using the Equipment Rental Service!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please try again.");
                        PauseForUser();
                        break;
                }
            }
        }

        private static void AddUserMenu()
        {
            Console.WriteLine("\n--- Add User ---");
            Console.Write("User Type (Student/Employee): ");
            string? userType = Console.ReadLine();
            Console.Write("First Name: ");
            string? firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string? lastName = Console.ReadLine();

            if (!string.IsNullOrEmpty(userType) && !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                Controller.AddUser(userType, firstName, lastName);
            }
            else
            {
                Console.WriteLine("Invalid input. Please provide all required fields.");
            }
            PauseForUser();
        }

        private static void AddEquipmentMenu()
        {
            Console.WriteLine("\n--- Add Equipment ---");
            Console.WriteLine("Equipment Types: Laptop, Projector, Camera");
            Console.Write("Equipment Type: ");
            string? equipmentType = Console.ReadLine();
            Console.Write("Name: ");
            string? name = Console.ReadLine();
            Console.Write("Bought Price: ");
            double boughtPrice = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Rental Price per Day: ");
            double rentalPrice = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Bought Date (yyyy-MM-dd): ");
            DateOnly boughtDate = DateOnly.Parse(Console.ReadLine() ?? DateTime.Now.ToString("yyyy-MM-dd"));

            if (equipmentType?.ToLower() == "laptop")
            {
                Console.Write("RAM (GB): ");
                int ram = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Storage (GB): ");
                int storage = int.Parse(Console.ReadLine() ?? "0");
                Controller.AddEquipment(equipmentType, name!, boughtPrice, rentalPrice, boughtDate, ram: ram, storage: storage);
            }
            else if (equipmentType?.ToLower() == "projector")
            {
                Console.Write("Brightness (Lumen): ");
                int brightness = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Has Bluetooth (true/false): ");
                bool hasBluetooth = bool.Parse(Console.ReadLine() ?? "false");
                Controller.AddEquipment(equipmentType, name!, boughtPrice, rentalPrice, boughtDate, brightnessLumen: brightness, isBluetooth: hasBluetooth);
            }
            else if (equipmentType?.ToLower() == "camera")
            {
                Console.Write("Max Resolution: ");
                string? maxResolution = Console.ReadLine();
                Console.Write("Camera Type: ");
                string? cameraType = Console.ReadLine();
                Controller.AddEquipment(equipmentType, name!, boughtPrice, rentalPrice, boughtDate, maxResolution: maxResolution, cameraType: cameraType);
            }

            Console.WriteLine("Equipment added successfully!");
            PauseForUser();
        }

        private static void RentEquipmentMenu()
        {
            Console.WriteLine("\n--- Rent Equipment ---");
            Console.Write("User Name: ");
            string? userName = Console.ReadLine();
            Console.Write("Equipment ID: ");
            int equipmentId = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Start Date (yyyy-MM-dd): ");
            DateOnly startDate = DateOnly.Parse(Console.ReadLine() ?? DateTime.Now.ToString("yyyy-MM-dd"));
            Console.Write("End Date (yyyy-MM-dd): ");
            DateOnly endDate = DateOnly.Parse(Console.ReadLine() ?? DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"));

            bool success = Controller.RentEquipment(userName!, equipmentId, startDate, endDate);
            Console.WriteLine(success ? "\n✓ Rental successful!" : "\n✗ Rental failed. Check availability and user limits.");
            PauseForUser();
        }

        private static void ReturnEquipmentMenu()
        {
            Console.WriteLine("\n--- Return Equipment ---");
            Console.Write("Rental ID: ");
            string? rentalId = Console.ReadLine();
            Console.Write("Return Date (yyyy-MM-dd): ");
            DateOnly returnDate = DateOnly.Parse(Console.ReadLine() ?? DateTime.Now.ToString("yyyy-MM-dd"));

            bool success = Controller.ReturnEquipment(rentalId!, returnDate);
            Console.WriteLine(success ? "\n✓ Return processed successfully!" : "\n✗ Return failed.");
            PauseForUser();
        }

        private static void MarkUnavailableMenu()
        {
            Console.WriteLine("\n--- Mark Equipment as Unavailable ---");
            Console.Write("Equipment ID: ");
            int equipmentId = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Reason: ");
            string? reason = Console.ReadLine();

            bool success = Controller.MarkEquipmentUnavailable(equipmentId, reason ?? "Not specified");
            Console.WriteLine(success ? "\n✓ Equipment marked as unavailable!" : "\n✗ Operation failed.");
            PauseForUser();
        }

        private static void DisplayActiveRentalsMenu()
        {
            Console.WriteLine("\n--- Display Active Rentals ---");
            Console.Write("User Name: ");
            string? userName = Console.ReadLine();

            Controller.DisplayActiveRentalsForUser(userName);
            PauseForUser();
        }

        private static void PauseForUser()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}