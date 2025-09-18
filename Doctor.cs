using System;
using System.Net.Http.Headers;

public class Doctor:User
{
    private int DoctorId { get; set; }
    private Appointments appointments;
    public bool RunLoop { get; set; } = true;

    public Doctor(int id, char role) : base(id, role)
    {
        this.DoctorId = id;
        this.appointments = new Appointments(this.DoctorId);
    }
    public Doctor(string[] parts) : base(Convert.ToInt32(parts[1]), Convert.ToChar(parts[0]))
    {
        this.DoctorId = Convert.ToInt32(parts[1]);
        this.FName = parts[2];
        this.Address = parts[3];
        this.Email = parts[4];
        this.PhoneNumber = Convert.ToInt32(parts[5]);
    }

    public void ShowMenu()
    {
        while (RunLoop)
        {
            Header.Show("Doctor Menu");
            Header.ResizeWindow(100, 25);
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. List Doctor Details");
            Console.WriteLine("2. List patients");
            Console.WriteLine("3. List appointments");
            Console.WriteLine("4. Check particular patient");
            Console.WriteLine("5. List appointments with patient");
            Console.WriteLine("6. Logout");
            Console.WriteLine("7. Exit");
            Console.Write("Select an option: ");
            Console.ReadLine();
            char choice = Convert.ToChar(Console.Read());
            switch (choice)
            {
                case '1':
                    Header.Show("My Details");
                    Header.ResizeWindow(100, 50);
                    Console.WriteLine("\n");
                    ViewDetails();
                    Console.ReadKey();
                    break;
                case '2':
                    Header.Show("My Patients");
                    Header.ResizeWindow(100, 50);
                    Console.WriteLine($"Patients assigned to {FName}:\n");
                    Header.PrintPatient();
                    ViewMyPatients();
                    Console.ReadKey();
                    break;
                case '3':
                    Header.Show("All Patients");
                    Header.ResizeWindow(100, 50);
                    GetAppointments();
                    Console.ReadKey();
                    break;
                case '4':
                    Header.ResizeWindow(130, 50);
                    Header.Show("Check Patient Details");
                    Console.Write("\nEnter the ID of the patient to check: ");
                    Console.ReadLine();
                    string line = Console.ReadLine();
                    if (!int.TryParse(line, out int input))
                    {
                        Console.WriteLine("Invalid input. Please enter a numeric ID.");
                        return;
                    }
                    Console.WriteLine("\n");    
                    Header.PrintPatientWithDoctor();

                    SearchPatient(input);
                    Console.ReadKey();
                    break;
                case '5':
                    Header.ResizeWindow(130, 50);
                    Header.Show("Appointments with");
                    Console.Write("\nEnter the ID of the patient you would like to view appointments for: ");
                    Console.ReadLine();
                    string line1 = Console.ReadLine();
                    if (!int.TryParse(line1, out int input1))
                    {
                        Console.WriteLine("Invalid input. Please enter a numeric ID.");
                        return;
                    }
                    Console.WriteLine("\n");
                    Header.PrintPatientWithDoctor();

                    GetPatientAppointments(input1);
                    Console.ReadKey();
                    break;
                case '6':
                    RunLoop = false; // Exit to login
                    break;
                case '7':
                    System.Environment.Exit(0);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
    public override void ViewDetails()
    {
        Header.PrintDoctor();
        Console.WriteLine(ToString());
    }
    public override string ToString()
    {
        return $"{FName,-20} | {Email,-30} | {PhoneNumber,-15} | {Address,-30}";
    }
    // Private methods
    private void GetPatientAppointments(int patientID)
    {
        Appointments a = new Appointments(patientID);
        a.List(GetFullName(patientID), FName);
    }
    private void ViewMyPatients()
    {
        string[] lines = GetUsersFile();
        foreach (var line in lines)
        {
            var parts = line.Split('\t');
            if (Convert.ToChar(parts[0]) == 'p' && Convert.ToInt32(parts[6]) == DoctorId)
            {
                Patient p = new Patient(parts);
                Console.WriteLine(p);
            }
        }
    }
    private void GetAppointments()
    {
        appointments.ListForDoctor(FName);

    }

}
