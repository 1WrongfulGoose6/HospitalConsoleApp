using System.Reflection.Metadata;

namespace HospitalConsoleApp
{
    internal class Program
    {
        public static bool RunLoop {get; set; } = true;
        static void Main(string[] args)
        {
            while (true)
            {
                Login login = new Login();
                while (!login.IsAuthenticated)
                {
                    login.Authenticate();
                }
                RunLoop = true;
                FileStream newFile = new FileStream("Users.txt", FileMode.OpenOrCreate, FileAccess.Read);
                newFile.Close();
                while (RunLoop)
                {
                    // After successful authentication, direct user based on role
                    int id = login.id;
                    char role = login.role;
                    switch (role)
                    {
                        case 'a':
                            Admin admin = new Admin(id, role);
                            admin.ShowMenu();
                            RunLoop = admin.RunLoop;
                            break;
                        case 'd':
                            Doctor doctor = new Doctor(id, role);
                            doctor.ShowMenu();
                            RunLoop = doctor.RunLoop;
                            break;
                        case 'p':
                            Patient patient = new Patient(id, role);
                            patient.ShowMenu();
                            RunLoop = patient.RunLoop;
                            break;
                        default:
                            Console.WriteLine("Unknown role. Access denied.");
                            break;
                    }
                }
            }
        }
    }
}
