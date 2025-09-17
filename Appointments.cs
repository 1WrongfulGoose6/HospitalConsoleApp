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

		using (StreamWriter writer = new StreamWriter("appointments.txt", append: true))
		{
			try
			{
				writer.WriteLine($"{UserID}\t{DoctorID}\t{Description}");
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				return false;
			}
		}
	}
	public void List(string PatientName, string DoctorName)
	{
		Header.Show("My Appointments");
		Console.WriteLine("\t");
		Header.Appointment();
		var lines = GetAppointmentsFile();
		foreach (var line in lines)
		{
			var parts = line.Split('\t');
			if (Convert.ToInt32(parts[0]) == UserID)
			{
				Header.Appointment(DoctorName, PatientName, parts[2]);
			}
		}
	}

	public void ListForDoctor(string doctorName)
	{
		Header.Show("My Appointments");
		Console.WriteLine("\t");
		Header.Appointment();

		var lines = GetAppointmentsFile();
		foreach (var line in lines)
		{
			var parts = line.Split('\t');
			int patientId = Convert.ToInt32(parts[0]);
			int doctorId = Convert.ToInt32(parts[1]);
			string description = parts[2];

			if (doctorId == UserID)
			{
				string patientName = GetPatientNameById(patientId);
				Header.Appointment(doctorName, patientName, description);
			}
		}
	}
    public void ViewbyDoctor()
	{
		var lines = GetAppointmentsFile();
		foreach (var line in lines)
		{
			var parts = line.Split('\t');
			Console.WriteLine($"PatientID: {parts[0]}, DoctorID: {parts[1]}, Description: {parts[2]}");
		}
	}

    private string GetPatientNameById(int patientId)
    {
        string[] lines = File.ReadAllLines("Users.txt");
        foreach (var line in lines)
        {
            var parts = line.Split('\t');
            if (parts[0] == "p" && Convert.ToInt32(parts[1]) == patientId)
            {
                return parts[2];
            }
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
		catch (FileNotFoundException)
		{
			Console.WriteLine("Appointments file not found.");
		}
		catch (IOException ex)
		{
			Console.WriteLine($"File error: {ex.Message}");
		}
		return lines;



	}

}