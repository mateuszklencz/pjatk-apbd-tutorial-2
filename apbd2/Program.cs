using System;

class Program
{
    static void Main(string[] args)
    {
        // Create rental date and other date references
        DateOnly rentalDate = new DateOnly(2026, 3, 28);
        DateOnly dueDate = new DateOnly(2026, 4, 4);
        DateOnly boughtDate = new DateOnly(2025, 1, 15);

        // Create a Student
        Student student = new Student("John", "Doe");

        // Create an Employee
        Employee employee = new Employee("Jane", "Smith");

        // Create a Laptop
        Laptop laptop = new Laptop("Dell XPS 15", 16, 512, 1200.00, 50.00, boughtDate);

        // Create a Camera
        Camera camera = new Camera("Canon EOS R5", "6000x4000", "Professional", 3999.99, 150.00, boughtDate);

        // Create a Projector
        Projector projector = new Projector("Epson EB-2250U", 5000, true, 2500.00, 100.00, boughtDate);

        // Display equipment information
        Console.WriteLine("=== Equipment Information ===\n");
        laptop.DisplayInfo();
        Console.WriteLine();
        camera.DisplayInfo();
        Console.WriteLine();
        projector.DisplayInfo();


        // Display user information
        Console.WriteLine("=== User Information ===\n");
        student.DisplayInfo();
        Console.WriteLine();
        employee.DisplayInfo();
        Console.WriteLine();

        // Create 3 rental instances
        var rental1 = new Rental(rentalDate, dueDate, student, laptop);
        var rental2 = new Rental(rentalDate.AddDays(2), dueDate.AddDays(5), employee, projector, dueDate.AddDays(6)); // with actual return
        var rental3 = new Rental(new DateOnly(2026, 4, 1), new DateOnly(2026, 4, 7), student, camera);

        // Summary of created rentals (avoid accessing equipment private members)
        Console.WriteLine("=== Rentals Created ===\n");
        rental1.DisplayInfo();
        Console.WriteLine();
        rental2.DisplayInfo();
        Console.WriteLine();
        rental2.DisplayInfo();


    }
}