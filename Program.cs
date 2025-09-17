using System.Reflection.Metadata;

namespace HospitalConsoleApp
{
    internal class Program
    {
        public static bool programLoop {get; set; } = true;
        static void Main(string[] args)
        {
            while (true)
            {
                Login login = new Login();
                while (!login.IsAuthenticated)
                {
                    login.Authenticate();
                }
                programLoop = true;
                FileStream newFile = new FileStream("Users.txt", FileMode.OpenOrCreate, FileAccess.Read);
                newFile.Close();
                while (programLoop)
                {
                    // After successful authentication, direct user based on role
                    int id = login.id;
                    char role = login.role;
                    switch (role)
                    {
                        case 'a':
                            Admin admin = new Admin(id, role);
                            admin.ShowMenu();
                            programLoop = admin.programLoop;
                            break;
                        case 'd':
                            Doctor doctor = new Doctor(id, role);
                            doctor.ShowMenu();
                            programLoop = doctor.programLoop;
                            break;
                        case 'p':
                            Patient patient = new Patient(id, role);
                            patient.ShowMenu();
                            programLoop = patient.programLoop;
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
