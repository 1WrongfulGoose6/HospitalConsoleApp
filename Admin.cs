using System;
using System.Reflection.Metadata;

public class Admin : User
{
    public bool programLoop { get; set; } = true;
    public Admin(int id, char role) : base(id, role) 
    {
        
    }


	public void ShowMenu()
	{
		while (programLoop)
		{
            Header.Show("Administrator Menu");
            Header.ResizeWindow(100, 25);
            Console.WriteLine($"Welcome to the DOTNET Hospital Management System {FName}");
            Console.WriteLine("Please choose an option:");
			Console.WriteLine("1. List All Doctors");
			Console.WriteLine("2. Check doctor details");
			Console.WriteLine("3. List all patients");
			Console.WriteLine("4. Check patient details");
            Console.WriteLine("5. add doctor");
            Console.WriteLine("6. Add patient");
            Console.WriteLine("7. Logout");
            Console.WriteLine("8. Exit");
            Console.Write("Select an option: ");
			var choice = Convert.ToChar(Console.Read());
			switch (choice)
			{
                case '1':
                    Header.Show("All Doctor");
                    Header.ResizeWindow(100, 25);
                    Console.WriteLine("All doctors registered to the DOTNET Hospital Management System\n");
                    Console.WriteLine("\n");
                    Header.PrintDoctor();
                    PrintAllDoctors();
                    Console.ReadLine();
                    Console.ReadKey();
                    break;
				case '2':
                    Header.ResizeWindow(130, 50);
                    Header.Show("Doctor Details");
                    Console.Write("\nPlease enter the ID of the doctor who's details you are checking: ");
                    Console.ReadLine();
                    string line = Console.ReadLine();
                    if (!int.TryParse(line, out int input))
                    {
                        Console.WriteLine("Invalid input. Please enter a numeric ID.");
                        return;
                    }
                    Console.WriteLine("\n");
                    Header.PrintDoctor();
                    SearchDoctor(input);
                    Console.ReadKey();
                    break;
				case '3':
                    Header.Show("All Patients");
                    Header.ResizeWindow(100, 25);
                    Console.WriteLine("All patients registered to the DOTNET Hospital Management System\n");
                    Console.WriteLine("\n");
                    Header.PrintPatient();
                    PrintAllPatients();
                    Console.ReadLine();
                    Console.ReadKey();
                    break;
				case '4':
                    Header.ResizeWindow(130, 50);
                    Header.Show("Patient Details");
                    Console.Write("\nPlease enter the ID of the patient who's details you are checking: ");
                    Console.ReadLine();
                    string line1 = Console.ReadLine();
                    if (!int.TryParse(line1, out int input1))
                    {
                        Console.WriteLine("Invalid input. Please enter a numeric ID.");
                        return;
                    }
                    Console.WriteLine("\n");
                    Header.PrintPatientWithDoctor();
                    SearchPatient(input1);
                    Console.ReadKey();
                    break;
                case '5':
                    Header.Show("Add Doctor");
                    Header.ResizeWindow(50, 25);
                    Console.ReadLine(); // flush leftover newline
                    AddDoctor();
                    Console.ReadKey();
                    break;
                case '6':
                    Header.Show("Add Patient");
                    Header.ResizeWindow(50, 25);
                    Console.ReadLine(); // flush leftover newline
                    AddPatient();
                    Console.ReadKey();
                    break;
                case '7':
                    programLoop = false; // Exit to login
                    break;
                case '8':
                    System.Environment.Exit(0);
                    return;
                default:
					Console.WriteLine("Invalid choice. Please try again.");
					break;
			}
		}
    }

    private void AddPatient()
    {
        string firstName = AddPrompt("First Name");
        string lastName = AddPrompt("Last Name");
        string email = AddPrompt("Email");
        string phone = AddPrompt("Phone");
        string streetNumber = AddPrompt("Street Number");
        string street = AddPrompt("Street");
        string city = AddPrompt("City");
        string state = AddPrompt("State");

        string address = $"{streetNumber} {street}, {city}, {state}";
        string fullName = $"{firstName} {lastName}";
        int newId = GetNextUserId();
        string line = $"p\t{newId}\t{fullName}\t{address}\t{email}\t{phone}\t0";
        File.AppendAllText("Users.txt", Environment.NewLine + line);
    }

    private void AddDoctor()
    {
        string firstName = AddPrompt("First Name");
        string lastName = AddPrompt("Last Name");
        string email = AddPrompt("Email");
        string phone = AddPrompt("Phone");
        string streetNumber = AddPrompt("Street Number");
        string street = AddPrompt("Street");
        string city = AddPrompt("City");
        string state = AddPrompt("State");

        string address = $"{streetNumber} {street}, {city}, {state}";
        string fullName = $"{firstName} {lastName}";
        int newId = GetNextUserId();
        string line = $"d\t{newId}\t{fullName}\t{address}\t{email}\t{phone}";
        File.AppendAllText("Users.txt", Environment.NewLine + line);
    }

    private string AddPrompt(string fieldName)
    {
        Console.Write($"{fieldName}: ");
        return Console.ReadLine();
    }
    private int GetNextUserId()
    {
        string[] lines = File.ReadAllLines("Users.txt");
        int maxId = 0;

        foreach (var line in lines)
        {
            var parts = line.Split('\t');
            int id = Convert.ToInt32(parts[1]);
            if (id > maxId) maxId = id;
        }

        return maxId + 1;
    }
    private void PrintAllPatients()
    {
        Patient[] patients = GetAllPatients();
        foreach (Patient p in patients)
        {
            Console.WriteLine(p);
        }
    }
    private void PrintAllDoctors()
    {
        Doctor[] doctors = GetAllDoctors();
        foreach (Doctor d in doctors)
        {
            Console.WriteLine(d);
        }
    }
}
