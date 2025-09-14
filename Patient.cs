using System;
using System.ComponentModel.DataAnnotations;
using System.Data;


public class Patient : User
{
    // Patient-specific properties
    private int patientId { get; set; }
    //private string fullName { get; set; }
    //private string address { get; set; }
    //private string email { get; set; }
    //private string phoneNumber { get; set; }
    //private string doctorAssigned { get; set; }


    public Patient(int id, char role) : base(id, role)
    {
        this.patientId = id;
        ShowMenu();
    }


    public void ShowMenu()
    {
        while (true)
        {
            Header.Show("Patient Menu");
            Header.ResizeWindow(100, 25);
            Console.WriteLine($"Welcome to the DOTNET Hospital Management System {fName}");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. List patient details");
            Console.WriteLine("2. List my doctor details");
            Console.WriteLine("3. List all appointments");
            Console.WriteLine("4. Book appointment");
            Console.WriteLine("5. Exit to login");
            Console.WriteLine("6. Exit System");
            Console.Write("Select an option: ");
            char choice = Convert.ToChar(Console.Read());
            switch (choice)
            {
                case '1':
                    ViewDetails();
                    break;
                case '2':
                    ViewDoctor();
                    break;
                case '3':
                    Console.WriteLine("Listing all patients...");
                    break;
                case '4':
                    Console.WriteLine("checking detailsout...");
                    return;
                case '5':
                    Console.WriteLine("adding doctor");
                    return;
                case '6':
                    System.Environment.Exit(0);
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public void ViewDetails()
    {
        Header.Show("My Details");
        Header.ResizeWindow(50, 25);

        Console.WriteLine("\n");
        Console.WriteLine($"{fName}'s Details:");
        Console.WriteLine("\n");
        Console.WriteLine($"Patient ID: {patientId}");
        Console.WriteLine($"Full Name: {fName}");
        Console.WriteLine($"Address: {Address}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine($"Phone Number: {PhoneNumber}");
        Console.WriteLine($"Doctor Assigned: {assignedDoctor}");
        Console.ReadKey();
    }

    public void ViewDoctor()
    {
        Header.Show("My Doctor");
        Header.ResizeWindow(150, 25);

        Console.WriteLine("\n");
        Header.PrintDoctor(FindDoctorData(assignedDoctor));
        Console.ReadKey();
    }

}
