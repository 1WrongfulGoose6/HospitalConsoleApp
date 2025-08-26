using System.Reflection.Metadata;

namespace HospitalConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Login login = new Login();
            FileStream newFile = new FileStream("Users.txt", FileMode.OpenOrCreate, FileAccess.Read);
            newFile.Close();
            while (true)
            {
                (var id, var role) = login.Authenticate();
                switch(role)
                {
                    case 'a':
                        Admin admin = new Admin(id);
                        admin.ShowMenu();
                        break;
                    case 'd':
                        Doctor doctor = new Doctor(id);
                        doctor.ShowMenu();
                        break;
                    case 'p':
                        Patient patient = new Patient(id);
                        patient.ShowMenu();
                        break;
                    default:
                        Console.WriteLine("Unknown role. Access denied.");
                        break;
                }


                }
        }

    }
}
