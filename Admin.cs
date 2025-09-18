using System;
using System.Reflection.Metadata;

public class Admin : User
{
    public bool RunLoop { get; set; } = true;
    public Admin(int id, char role) : base(id, role) 
    {
        
    }

	public void ShowMenu()
	{
		while (RunLoop)
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
                    RunLoop = false; // Exit to login
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
        string firstName, lastName, email, phone, streetNumber, street, city, state;

        while (true)
        {
            Console.Clear(); // reset screen
            Header.Show("Add User");
            Header.ResizeWindow(50, 25);

            firstName = AddPrompt("First Name");
            lastName = AddPrompt("Last Name");
            email = AddPrompt("Email");
            phone = AddPrompt("Phone");
            streetNumber = AddPrompt("Street Number");
            street = AddPrompt("Street");
            city = AddPrompt("City");
            state = AddPrompt("State");

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(streetNumber) ||
                string.IsNullOrWhiteSpace(street) ||
                string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(state))
            {
                Console.WriteLine("\nPlease enter valid data for all fields.");
                Console.WriteLine("Press any key to try again.");
                Console.ReadKey();
                continue;
            }

            break;
        }

        string address = $"{streetNumber} {street}, {city}, {state}";
        string fullName = $"{firstName} {lastName}";
        int newId = CreateUserID();
        string line = $"p\t{newId}\t{fullName}\t{address}\t{email}\t{phone}\t0";

        // Check if file is empty
        bool fileIsEmpty = new FileInfo("Users.txt").Length == 0;
        File.AppendAllText("Users.txt", line + (fileIsEmpty ? "" : Environment.NewLine));

        // Add to credentials with default password
        string defaultPassword = "password";
        string credentialsLine = $"{newId}\t{defaultPassword}\tp";
        File.AppendAllText("Credentials.txt", credentialsLine + (fileIsEmpty ? "" : Environment.NewLine));
    }
    private void AddDoctor()
    {

        string firstName, lastName, email, phone, streetNumber, street, city, state;

        while (true)
        {
            Console.Clear(); // reset screen
            Header.Show("Add User");
            Header.ResizeWindow(50, 25);

            firstName = AddPrompt("First Name");
            lastName = AddPrompt("Last Name");
            email = AddPrompt("Email");
            phone = AddPrompt("Phone");
            streetNumber = AddPrompt("Street Number");
            street = AddPrompt("Street");
            city = AddPrompt("City");
            state = AddPrompt("State");

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(streetNumber) ||
                string.IsNullOrWhiteSpace(street) ||
                string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(state))
            {
                Console.WriteLine("\nPlease enter valid data for all fields.");
                Console.WriteLine("Press any key to try again.");
                Console.ReadKey();
                continue; 
            }

            break;
        }
        string address = $"{streetNumber} {street}, {city}, {state}";
        string fullName = $"{firstName} {lastName}";
        int newId = GenerateNewId();
        string line = $"d\t{newId}\t{fullName}\t{address}\t{email}\t{phone}";

        // Check if file is empty
        bool fileIsEmpty = new FileInfo("Users.txt").Length == 0;
        File.AppendAllText("Users.txt", line + (fileIsEmpty ? "" : Environment.NewLine));

        // Add to credentials with default password
        string defaultPassword = "password";
        string credentialsLine = $"{newId}\t{defaultPassword}\td";
        File.AppendAllText("Credentials.txt", credentialsLine + (fileIsEmpty ? "" : Environment.NewLine));
    }
    private string AddPrompt(string fieldName)
    {
        Console.Write($"{fieldName}: ");
        return Console.ReadLine();
    }
    private int CreateUserID()
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
    public static int GenerateNewId(string usersFile = "users.txt")
    {
        int MinId = 10000000; // First valid 8-digit ID
        try
        {
            var lines = File.ReadAllLines(usersFile);

            var ids = lines
                .Select(line =>
                {
                    var parts = line.Split('\t');
                    if (parts.Length >= 2 && int.TryParse(parts[1], out int id))
                        return id;
                    return -1; // invalid ID format
                })
                .Where(id => id >= MinId) // only count valid IDs
                .ToList();

            if (!ids.Any())
            {
                return MinId; // first ID
            }

            return ids.Max() + 1; // next unique ID
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating user ID: {ex.Message}");
            return MinId; // fallback
        }
    }
}
