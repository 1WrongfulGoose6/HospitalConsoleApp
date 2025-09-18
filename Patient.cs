using HospitalConsoleApp;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.CompilerServices;


public class Patient : User
{
    // Patient-specific properties
    private int patientId { get; set; }
    private int AssignedDoctor { get; set; }
    private Appointments appointments;
    public bool RunLoop { get; set; } = true;

    // Constructor to that acts as a manager
    public Patient(int id, char role) : base(id, role)
    {
        this.patientId = id;
        this.AssignedDoctor = GetAssignedDoctor();
        this.appointments = new Appointments(this.patientId);
    }
    // Overloaded constructor to initialize from string array
    public Patient(string[] parts) : base(Convert.ToInt32(parts[1]), Convert.ToChar(parts[0]))
    {
        this.patientId = Convert.ToInt32(parts[1]);
        this.FName = parts[2];
        this.Address = parts[3];
        this.Email = parts[4];
        this.PhoneNumber = Convert.ToInt32(parts[5]);
        this.AssignedDoctor = parts[6] == "Null" ? -1 : Convert.ToInt32(parts[6]);
    }
    public void ShowMenu()
    {
        while (RunLoop)
        {
            Header.Show("Patient Menu");
            Header.ResizeWindow(100, 25);
            Console.WriteLine($"Welcome to the DOTNET Hospital Management System {FName}");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. List patient details");
            Console.WriteLine("2. List my doctor details");
            Console.WriteLine("3. List all appointments");
            Console.WriteLine("4. Book appointment");
            Console.WriteLine("5. Exit to login");
            Console.WriteLine("6. Exit System");
            Console.Write("Select an option: ");
			var choice = Convert.ToChar(Console.Read());
            switch (choice)
            {
                case '1':
                    Header.Show("My Details");
                    Header.ResizeWindow(50, 25);
                    Console.WriteLine("\n");
                    ViewDetails();
                    Console.ReadKey();
                    break;
                case '2':
                    Header.Show("My Doctor");
                    Header.ResizeWindow(100, 25);
                    Console.WriteLine("\n");
                    ViewDoctor();
                    Console.ReadKey();
                    break;
                case '3':
                    Header.Show("My Appointments");
                    Header.ResizeWindow(150, 25);
                    GetAppointments();
                    Console.ReadKey();
                    break;
                case '4':
                    CreateAppointment();
                    break;
                case '5':
                    RunLoop = false; // Exit to login
                    break;
                case '6':
                    System.Environment.Exit(0);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }
    public override string ToString()
    {
        return $"{FName,-20} | {Email,-30} | {PhoneNumber,-15} | {Address,-30}";
    }
    public string ToStringWithDoctor()
    {
        return $"{FName,-20} | {GetFullName(AssignedDoctor),-20} |{Email,-30} | {PhoneNumber,-15} | {Address,-30}";
    }
    public override void ViewDetails()
    {
        Console.WriteLine($"{FName}'s Details:");
        Console.WriteLine("\n");
        Console.WriteLine($"Patient ID: {patientId}");
        Console.WriteLine($"Full Name: {FName}");
        Console.WriteLine($"Address: {Address}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine($"Phone Number: {PhoneNumber}");
    }
    // private methods
    private void GetAppointments()
    {
        if (AssignedDoctor == 0)
        {
            AssignDoctor();
            Console.Clear();
            Header.Show("My Appointment");
            Header.ResizeWindow(100, 50);
        }
        appointments.List(FName, GetFullName(AssignedDoctor));
    }
    private void CreateAppointment()
    {
        Console.Clear();
        Header.Show("Book Appointment");
        Header.ResizeWindow(100, 25);

        if (AssignedDoctor == 0)
        {
            AssignDoctor();
            Console.Clear();
            Header.Show("Book Appointment");
            Header.ResizeWindow(100, 50);
        }
        Console.WriteLine($"You are booking a new appointment with {GetFullName(AssignedDoctor)}\t");
        Console.WriteLine("Description of the appointment:");
        Console.ReadLine();
        string description = Console.ReadLine();
        try
        {
            if (appointments.Book(this.AssignedDoctor, description))
            {
                Console.WriteLine("Appointment booked successfully.");
            }
            else
            {
                Console.WriteLine("Failed to book appointment. Please try again.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error booking appointment: {ex.Message}");
        }
    Console.ReadKey();
    }
    private void AssignDoctor()
    {
        Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with");
        Doctor[] doctors = GetAllDoctors();

        // Print all doctors
        int index = 1;
        foreach (Doctor d in doctors)
        {
            Console.Write($"{index}.{d}");
            index++;
        }
        Console.WriteLine("Please choose a doctor:");
        Console.ReadLine();

        //Assign doctor to patient
        int choice = -1;
        bool valid = false;

        while (!valid)
        {
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
                if (choice < 1 || choice > doctors.Length)
                {
                    throw new ArgumentOutOfRangeException();
                }
                valid = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number:");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Invalid choice. Please choose a valid doctor number:");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}. Please try again:");
            }
        }
        Doctor selectedDoctor = doctors[choice - 1];
        this.AssignedDoctor = selectedDoctor.UserID;
        UpdateAssignedDoctor();
    }
    private void UpdateAssignedDoctor()
    {
        try
        {
            // Update the user's assigned doctor in the Users.txt file
            string[] lines = GetUsersFile();
            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split('\t');
                if (parts[0] == "p" && Convert.ToInt32(parts[1]) == this.patientId)
                {
                    parts[6] = AssignedDoctor.ToString();
                    lines[i] = string.Join("\t", parts);
                    break;
                }
            }
            File.WriteAllLines("Users.txt", lines);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"File error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

    }
    private void ViewDoctor()
    {
        if (AssignedDoctor == 0)
        {
            AssignDoctor();
            Console.Clear();
            Header.Show("My Doctor");
            Header.ResizeWindow(100, 50);
        }

        Header.PrintDoctor();
        string[] lines = GetUsersFile();
        foreach (var line in lines)
        {
            var parts = line.Split('\t');
            if (Convert.ToChar(parts[0]) == 'd' && Convert.ToInt32(parts[1]) == AssignedDoctor)
            {
                Doctor d = new Doctor(parts);
                Console.WriteLine(d);
                break;
            }
        }
    }
}
