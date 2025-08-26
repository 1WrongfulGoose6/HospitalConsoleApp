using System;

public class Patient : User
{

	public Patient(int id) : base(id){}


    public void ShowMenu()
    {
        while (true)
        {
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
            char choice = Convert.ToChar(Console.Read());
            switch (choice)
            {
                case '1':
                    Console.WriteLine("Listing all doctors...");
                    break;
                case '2':
                    Console.WriteLine("checking details out...");
                    break;
                case '3':
                    Console.WriteLine("Listing all patients...");
                    break;
                case '4':
                    Console.WriteLine("checking detailsout...");
                    return;
                case '5':
                    Console.WriteLine("adding doctor");
                    return;
                case '6':
                    Console.WriteLine("adding patient");
                    return;
                case '7':
                    Console.WriteLine("Logging out");
                    return;
                case '8':
                    Console.WriteLine("Exiting");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

}
