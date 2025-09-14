using System;
using System.Xml.Linq;

public abstract class User
{
    public char Role { get; set; }
    public int UserID { get; private set; }
    public string fName { get; private set; }
    public int PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public int assignedDoctor { get; private set; }


    public User(int userId, char Role)
    {
        this.UserID = userId;
        this.Role = Role;
        if (Role == 'p')
            LoadPatientData();
        else if (Role == 'd')
            LoadDoctorData();
        else
            Console.WriteLine("Invalid role specified.");
    }

    private void LoadPatientData()
    {
        string[] lines = Array.Empty<string>();

        try
        {
            lines = File.ReadAllLines("Users.txt");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Users File Missing. Please Replace and Try Again");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"File error: {ex.Message}");
        }

        foreach (var line in File.ReadLines("users.txt"))
        {
            var parts = line.Split('\t');
            if (Convert.ToChar(parts[0])=='p' && Convert.ToInt32(parts[1]) == this.UserID)
            {
                this.fName = parts[2];
                this.Address = parts[3];
                this.Email = parts[4];
                this.PhoneNumber = Convert.ToInt32(parts[5]);
                this.assignedDoctor = Convert.ToInt32(parts[6]);
                Console.WriteLine("Patient data loaded successfully.");
                break;
            }
        }
    }


    private void LoadDoctorData()
    {
        string[] lines = Array.Empty<string>();

        try
        {
            lines = File.ReadAllLines("Users.txt");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Users File Missing. Please Replace and Try Again");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"File error: {ex.Message}");
        }

        foreach (var line in File.ReadLines("users.txt"))
        {
            var parts = line.Split('\t');
            if (Convert.ToChar(parts[0]) == 'd' && Convert.ToInt32(parts[1]) == this.UserID)
            {
                this.fName = parts[2];
                this.Address = parts[3];
                this.Email = parts[4];
                this.PhoneNumber = Convert.ToInt32(parts[5]);
                Console.WriteLine("Doctor data loaded successfully.");
                break;
            }
        }
    }
    public string FindDoctorData(int doctorID)
    {
        string[] lines = Array.Empty<string>();
        try
        {
            lines = File.ReadAllLines("Users.txt");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Users File Missing. Please Replace and Try Again");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"File error: {ex.Message}");
        }

        foreach (var line in File.ReadLines("users.txt"))
        {
            var parts = line.Split('\t');
            if (Convert.ToInt32(parts[1]) == doctorID)
            {
                return line;
            }
        }
        return "Doctor Not Found";
    }
}