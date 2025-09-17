using System;
using System.Xml.Linq;

public abstract class User
{
    public char Role { get; private set; }
    public int UserID { get; private set; }
    public string FName { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    //public int AssignedDoctor { get; private set; }
    public int[] AssignedPatient { get; set; }
    public virtual void ViewDetails() { }


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

    // Utility Methods 
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
            if (Convert.ToChar(parts[0]) == 'p' && Convert.ToInt32(parts[1]) == this.UserID)
            {
                this.FName = parts[2];
                this.Address = parts[3];
                this.Email = parts[4];
                this.PhoneNumber = Convert.ToInt32(parts[5]);
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
                this.FName = parts[2];
                this.Address = parts[3];
                this.Email = parts[4];
                this.PhoneNumber = Convert.ToInt32(parts[5]);
                break;
            }
        }
    }
    protected string[] GetUsersFile()
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
        return lines;
    }

    //Doctor Getters
    public string GetDoctor(int doctorID)
    {
        string[] lines = GetUsersFile();

        foreach (var line in File.ReadLines("users.txt"))
        {
            var parts = line.Split('\t');
            if (Convert.ToInt32(parts[1]) == doctorID)
            {
                return line;
            }
        }
        throw new InvalidOperationException("Doctor Not Found");
    }
    public int GetAssignedDoctor()
    {
        string[] lines = GetUsersFile();

        foreach (var line in File.ReadLines("users.txt"))
        {
            var parts = line.Split('\t');
            if (Convert.ToInt32(parts[1]) == UserID)
            {
                return Convert.ToInt32(parts[6]);
            }
        }
        throw new InvalidOperationException("Patient Not Found");
    }
    public Doctor[] GetAllDoctors()
    {
        string[] lines = GetUsersFile();
        // Count number of doctors
        int count = 0;
        foreach (string line in lines)
        {
            var parts = line.Split('\t');
            if (Convert.ToChar(parts[0]) == 'd')
            {
                count++;
            }
        }
        // Populate the doctors array with objects
        Doctor[] docs = new Doctor[count];
        int index = 0;
        foreach (string line in lines)
        {
            var parts = line.Split('\t');
            if (Convert.ToChar(parts[0]) == 'd')
            {
                docs[index++] = new Doctor(parts);
            }
        }
        return docs;
    }
    public Patient[] GetAllPatients()
    {
        string[] lines = GetUsersFile();
        // Count number of doctors
        int count = 0;
        foreach (string line in lines)
        {
            var parts = line.Split('\t');
            if (Convert.ToChar(parts[0]) == 'p')
            {
                count++;
            }
        }
        // Populate the doctors array with objects
        Patient[] p = new Patient[count];
        int index = 0;
        foreach (string line in lines)
        {
            var parts = line.Split('\t');
            if (Convert.ToChar(parts[0]) == 'p')
            {
                p[index++] = new Patient(parts);
            }
        }
        return p;
    }
    public string getFullName(int ID)
    {
        string[] lines = GetUsersFile();

        foreach (var line in File.ReadLines("users.txt"))
        {
            var parts = line.Split('\t');
            if (Convert.ToInt32(parts[1]) == ID)
            {
                return Convert.ToString(parts[2]);
            }
        }
        throw new InvalidOperationException("User Not Found");
    }
    public void SearchPatient(int patientID)
    {
        string[] lines = GetUsersFile();
        foreach (var line in lines)
        {
            var parts = line.Split('\t');
            if (Convert.ToChar(parts[0]) == 'p' && Convert.ToInt32(parts[1]) == patientID)
            {
                Patient p = new Patient(parts);
                Console.WriteLine(p.ToStringWithDoctor());
            }
        }
    }
    public void SearchDoctor(int doctorID)
    {
        string[] lines = GetUsersFile();
        foreach (var line in lines)
        {
            var parts = line.Split('\t');
            if (Convert.ToChar(parts[0]) == 'd' && Convert.ToInt32(parts[1]) == doctorID)
            {
                Doctor p = new Doctor(parts);
                Console.WriteLine(p);
            }
        }
    }
}

    