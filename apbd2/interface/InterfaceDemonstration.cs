using System;
using System.Linq;

namespace apbd2
{
    public static class InterfaceDemonstration
    {
        public static void RunDemonstration()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   UNIVERSITY EQUIPMENT RENTAL SERVICE - DEMONSTRATION         ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            // Scenario 1: Adding several equipment items of different types
            Console.WriteLine("┌───────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│ SCENARIO 1: Adding Equipment Items                            │");
            Console.WriteLine("└───────────────────────────────────────────────────────────────┘");
            AddEquipmentItems(); // 6 items added
            Console.WriteLine();

            // Scenario 2: Adding several users of different types
            Console.WriteLine("┌───────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│ SCENARIO 2: Adding Users                                      │");
            Console.WriteLine("└───────────────────────────────────────────────────────────────┘");
            AddUsers(); // 4 users added
            Console.WriteLine();

            // Display available equipment
            Console.WriteLine("┌───────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│ Available Equipment Before Rentals                            │");
            Console.WriteLine("└───────────────────────────────────────────────────────────────┘");
            Controller.DisplayAvailableEquipment();
            Console.WriteLine();

            // Scenario 3: A correct rental operation
            Console.WriteLine("┌───────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│ SCENARIO 3: Correct Rental Operation                          │");
            Console.WriteLine("└───────────────────────────────────────────────────────────────┘");
            CorrectRental();
            Console.WriteLine();

            // Scenario 4: An attempted invalid operation
            Console.WriteLine("┌───────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│ SCENARIO 4: Invalid Operations                                │");
            Console.WriteLine("└───────────────────────────────────────────────────────────────┘");
            InvalidOperations();
            Console.WriteLine();

            // Scenario 5: A return completed on time
            Console.WriteLine("┌───────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│ SCENARIO 5: Return Completed On Time                          │");
            Console.WriteLine("└───────────────────────────────────────────────────────────────┘");
            OnTimeReturn();
            Console.WriteLine();

            // Scenario 6: A delayed return that leads to a penalty
            Console.WriteLine("┌───────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│ SCENARIO 6: Delayed Return with Penalty                       │");
            Console.WriteLine("└───────────────────────────────────────────────────────────────┘");
            DelayedReturn();
            Console.WriteLine();

            // Scenario 7: Displaying a final report of the system state
            Console.WriteLine("┌───────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│ SCENARIO 7: Final System Report                               │");
            Console.WriteLine("└───────────────────────────────────────────────────────────────┘");
            Controller.GenerateSummaryReport();
            Console.WriteLine();

            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   DEMONSTRATION COMPLETED                                     ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
        }

        private static void AddEquipmentItems()
        {
            Console.WriteLine("Adding Laptop..."); // 1
            Controller.AddEquipment(
                equipmentType: "Laptop",
                name: "Dell XPS 15",
                boughtPrice: 5000,
                rentalPrice: 50,
                boughtDate: new DateOnly(2023, 1, 15),
                ram: 16,
                storage: 512
            );

            Console.WriteLine("Adding Projector..."); // 2
            Controller.AddEquipment(
                equipmentType: "Projector",
                name: "Epson EB-2250U",
                boughtPrice: 3500,
                rentalPrice: 40,
                boughtDate: new DateOnly(2023, 3, 10),
                brightnessLumen: 5000,
                isBluetooth: true
            );

            Console.WriteLine("Adding Camera..."); // 3
            Controller.AddEquipment(
                equipmentType: "Camera",
                name: "Canon EOS R5",
                boughtPrice: 12000,
                rentalPrice: 80,
                boughtDate: new DateOnly(2023, 5, 20),
                maxResolution: "2096x1440",
                cameraType: "Amateur"
            );

            Console.WriteLine("Adding another Laptop..."); // 4
            Controller.AddEquipment(
                equipmentType: "Laptop",
                name: "MacBook Pro 16",
                boughtPrice: 8000,
                rentalPrice: 70,
                boughtDate: new DateOnly(2023, 6, 1),
                ram: 32,
                storage: 1024
            );

            Console.WriteLine("Adding another Camera..."); // 5
            Controller.AddEquipment(
                equipmentType: "Camera",
                name: "Sony A7 III",
                boughtPrice: 7000,
                rentalPrice: 60,
                boughtDate: new DateOnly(2023, 7, 15),
                maxResolution: "4096×3112 ",
                cameraType: "Professional"
            );

            Console.WriteLine("Adding Projector..."); // 6
            Controller.AddEquipment(
                equipmentType: "Projector",
                name: "Hisene C2",
                boughtPrice: 3500,
                rentalPrice: 40,
                boughtDate: new DateOnly(2025, 3, 10),
                brightnessLumen: 3000,
                isBluetooth: false
            );

            Console.WriteLine("\n✓ All equipment items added successfully.");
        }

        private static void AddUsers()
        {
            Console.WriteLine("Adding Student: Mateusz Klencz...");
            Controller.AddUser("Student", "Mateusz", "Klencz"); // username: mklenc

            Console.WriteLine("Adding Student: Anna Nowak..."); // username: anowak
            Controller.AddUser("Student", "Anna", "Nowak");

            Console.WriteLine("Adding Employee: Michael Smith..."); // username: msmith
            Controller.AddUser("Employee", "Michael", "Smith");

            Console.WriteLine("Adding Employee: Sarah Johnson..."); //  username: sjohns
            Controller.AddUser("Employee", "Sarah", "Johnson");

            Console.WriteLine("\n✓ All users added successfully."); 
        }

        private static void CorrectRental()
        {
            Console.WriteLine("Student 'mklenc' renting equipment ID 1 (Dell XPS 15)...");
            DateOnly startDate = new DateOnly(2026, 3, 20);
            DateOnly endDate = new DateOnly(2026, 3, 27);

            bool success = Controller.RentEquipment("mklenc", 1, startDate, endDate);
            if (success)
            {
                Console.WriteLine("✓ Rental successful!");
                Console.WriteLine($"  Equipment ID: 1");
                Console.WriteLine($"  User: mklencz");
                Console.WriteLine($"  Start Date: {startDate}");
                Console.WriteLine($"  Due Date: {endDate}");
            }
            else
            {
                Console.WriteLine("✗ Rental failed.");
            }
        }

        private static void InvalidOperations()
        {
            Console.WriteLine("Attempting invalid operations...\n");

            // Invalid Operation 1: Trying to rent already rented equipment
            Console.WriteLine("1. Trying to rent already rented equipment (ID 1):");
            DateOnly startDate1 = new DateOnly(2026, 3, 21);
            DateOnly endDate1 = new DateOnly(2026, 3, 28);
            bool result1 = Controller.RentEquipment("anowak", 1, startDate1, endDate1);
            Console.WriteLine(result1 ? "✓ Rental successful" : "✗ Rental failed - Equipment unavailable (as expected)");
            Console.WriteLine();

            // Invalid Operation 2: Trying to rent unavailable equipment
            Console.WriteLine("2. Marking equipment ID 2 as unavailable and trying to rent it:");
            Controller.MarkEquipmentUnavailable(2, "Under maintenance");
            DateOnly startDate2 = new DateOnly(2026, 3, 21);
            DateOnly endDate2 = new DateOnly(2026, 3, 25);
            bool result2 = Controller.RentEquipment("anowak", 2, startDate2, endDate2);
            Console.WriteLine(result2 ? "✓ Rental successful" : "✗ Rental failed - Equipment under maintenance (as expected)");
            Console.WriteLine();

            // Invalid Operation 3: Student exceeding rental limit (max 2)
            Console.WriteLine("3. Student 'mklenc' trying to exceed rental limit (max 2 rentals):");
            Console.WriteLine("   First rental (equipment ID 3)...");
            DateOnly startDate3a = new DateOnly(2026, 3, 22);
            DateOnly endDate3a = new DateOnly(2026, 3, 29);
            bool result3a = Controller.RentEquipment("mklenc", 3, startDate3a, endDate3a);
            Console.WriteLine(result3a ? "   ✓ First rental successful" : "   ✗ First rental failed");

            Console.WriteLine("   Second rental attempt (equipment ID 4)...");
            DateOnly startDate3b = new DateOnly(2026, 3, 22);
            DateOnly endDate3b = new DateOnly(2026, 3, 29);
            bool result3b = Controller.RentEquipment("mklenc", 4, startDate3b, endDate3b);
            Console.WriteLine(result3b ? "   ✓ Second rental successful" : "   ✗ Second rental failed");

            Console.WriteLine("   Third rental attempt (equipment ID 5)...");
            DateOnly startDate3c = new DateOnly(2026, 3, 22);
            DateOnly endDate3c = new DateOnly(2026, 3, 29);
            bool result3c = Controller.RentEquipment("mklenc", 5, startDate3c, endDate3c);
            Console.WriteLine(result3c ? "   ✓ Third rental successful" : "   ✗ Third rental failed - Student limit exceeded (as expected)");
        }

        private static void OnTimeReturn()
        {
            Console.WriteLine("Employee 'msmith' renting equipment ID 4 (MacBook Pro 16)...");
            DateOnly startDate = new DateOnly(2026, 3, 23);
            DateOnly endDate = new DateOnly(2026, 3, 26);

            bool rentalSuccess = Controller.RentEquipment("msmith", 4, startDate, endDate);
            if (rentalSuccess)
            {
                Console.WriteLine("✓ Rental successful!");
                Console.WriteLine($"  Equipment ID: 4");
                Console.WriteLine($"  User: msmith");
                Console.WriteLine($"  Due Date: {endDate}");
                Console.WriteLine();

                // Return on time
                Console.WriteLine($"Returning equipment on time (return date: {endDate})...");

                Controller.ReturnEquipment(
                    rentalId: "20260323-4-msmith",
                    returnDate: endDate
                );

                var okRental = RentalRepo.getRental("20260323-4-msmith");

                // Added null check and fixed the typo: CalculatePenalty
                if (okRental != null)
                {
                    double rentalFee = okRental.CalculatePenalty();
                    Console.WriteLine($"Penalty fee: {rentalFee} PLN (should be 0)");
                }
                else
                {
                    Console.WriteLine("✗ Error: Rental record not found after termination.");
                }
            }
        }

        private static void DelayedReturn()
        {
            Console.WriteLine("Employee 'sjohns' renting equipment ID 5 (Sony A7 III)...");
            DateOnly startDate = new DateOnly(2026, 3, 20);
            DateOnly endDate = new DateOnly(2026, 3, 25);

            bool rentalSuccess = Controller.RentEquipment("sjohns", 5, startDate, endDate);
            if (rentalSuccess)
            {
                Console.WriteLine("✓ Rental successful!");
                Console.WriteLine($"  Equipment ID: 5");
                Console.WriteLine($"  User: sjohns");
                Console.WriteLine($"  Due Date: {endDate}");
                Console.WriteLine();

                // Return with delay (3 days late)
                DateOnly lateReturnDate = new DateOnly(2026, 3, 28);
                int daysLate = lateReturnDate.DayNumber - endDate.DayNumber;
                Console.WriteLine($"Returning equipment {daysLate} days late (return date: {lateReturnDate})...");

                var allRentals = RentalRepo.getAllRentals();
                var rental = allRentals.FirstOrDefault(r => r.RentedTo.UserName == "sjohns" && r.RentedItem.Id == 5 && r.IsActive);

                if (rental != null)
                {
                    bool returnSuccess = Controller.ReturnEquipment(rental.Id, lateReturnDate);
                    Console.WriteLine(returnSuccess ? "✓ Return processed with late penalty applied!" : "✗ Return failed");
                }
            }
        }
    }
}