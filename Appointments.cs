using System;
using System.Security.Cryptography.X509Certificates;

public class Appointments
{
	public int UserID { get; set; }
	public int ForeignID { get; set; }
    public string Description { get; set; } = null;
	public Appointments(int UserID)
	{
		this.UserID = UserID;
	}

	public bool Book(int DoctorID, string Description)
	{
        try
        {
            using (StreamWriter writer = new StreamWriter("appointments.txt", append: true))
            {
                writer.WriteLine($"{UserID}\t{DoctorID}\t{Description}");
            }
            return true;
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($" File error while booking: {ioEx.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error booking appointment: {ex.Message}");
            return false;
        }
    }
	public void List(string PatientName, string DoctorName)
	{
        try
        {
            Header.Show("My Appointments");
            Console.WriteLine("\t");
            Header.Appointment();

            var lines = GetAppointmentsFile();
            foreach (var line in lines)
            {
                var parts = line.Split('\t');
                if (parts.Length < 3) continue; // skip malformed rows

                if (int.TryParse(parts[0], out int patientId) && patientId == UserID)
                {
                    Header.Appointment(DoctorName, PatientName, parts[2]);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error listing appointments: {ex.Message}");
        }
    }
	public void ListForDoctor(string doctorName)
	{
        try
        {
            Header.Show("My Appointments");
            Console.WriteLine("\t");
            Header.Appointment();

            var lines = GetAppointmentsFile();
            foreach (var line in lines)
            {
                var parts = line.Split('\t');
                if (parts.Length < 3) continue;

                if (int.TryParse(parts[0], out int patientId) &&
                    int.TryParse(parts[1], out int doctorId) &&
                    doctorId == UserID)
                {
                    string description = parts[2];
                    string patientName = GetPatientNameById(patientId);
                    Header.Appointment(doctorName, patientName, description);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠ Error listing doctor appointments: {ex.Message}");
        }
    }
    public void ViewbyDoctor()
	{
        try
        {
            var lines = GetAppointmentsFile();
            foreach (var line in lines)
            {
                var parts = line.Split('\t');
                if (parts.Length < 3) continue;

                Console.WriteLine($"PatientID: {parts[0]}, DoctorID: {parts[1]}, Description: {parts[2]}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error viewing by doctor: {ex.Message}");
        }
    }
    private string GetPatientNameById(int patientId)
    {
        try
        {
            string[] lines = File.ReadAllLines("Users.txt");
            foreach (var line in lines)
            {
                var parts = line.Split('\t');
                if (parts.Length < 3) continue;

                if (parts[0] == "p" && int.TryParse(parts[1], out int id) && id == patientId)
                {
                    return parts[2];
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Users file not found.");
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"File error while getting patient name: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error getting patient name: {ex.Message}");
        }
        return "Unknown Patient";
    }
	private string[] GetAppointmentsFile()

	{
		string[] lines = Array.Empty<string>();
		try
		{
			lines = File.ReadAllLines("appointments.txt");
		}
		catch (FileNotFoundException ex)
		{
			Console.WriteLine($"Appointments file not found: {ex.Message}");
		}
		catch (IOException ex)
		{
			Console.WriteLine($"File error: {ex.Message}");
		}
        catch(Exception ex)
        {
            Console.WriteLine($"Unexpected error reading appointments: {ex.Message}");
        }
        return lines;



	}

}