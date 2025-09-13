using System.Reflection.Metadata;

namespace HospitalConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Login login = new Login();
            while (!login.IsAuthenticated)
            {
                login.Authenticate();
            }
            FileStream newFile = new FileStream("Users.txt", FileMode.OpenOrCreate, FileAccess.Read);
            newFile.Close();
            while (true)
            {
                // After successful authentication, direct user based on role
                int id = login.id;
                char role = login.role;
                switch(role)
                {
                    case 'a':
                        Admin admin = new Admin(id);
                        Header.Show("Admin Menu");
                        admin.ShowMenu();
                        break;
                    case 'd':
                        Doctor doctor = new Doctor(id);
                        Header.Show("Doctor Menu");
                        doctor.ShowMenu();
                        break;
                    case 'p':
                        Patient patient = new Patient(id);
                        break;
                    default:
                        Console.WriteLine("Unknown role. Access denied.");
                        break;
                }


                }
        }

    }
}
