using System;
using System.IO;
using System.Text;

public class Login
{
    public bool IsAuthenticated { get; internal set; } // Property to track authentication status
    public int id { get; internal set; } // Property to store user ID
    public char role { get; internal set; } // Property to store user role
    private readonly string[] requiredFiles = { "Credentials.txt", "Users.txt", "Appointments.txt" };

    public Login()
    {
        IsAuthenticated = false;
        id = -1;
        role = ' ';
    }

    // Main authentication method
    public void Authenticate()
    {
        IsAuthenticated = false;
        string id = "";
        string password = "";

        int currentField = 0;
        bool done = false;

        bool onUser = true; // true = ID field, false = Password field

        // first loop checks fields, second loop validates credentials
        CheckFilesExist();
        while (!done)
            {
            Header.Show("Login");
            Header.ResizeWindow(50, 10);

            // ID field
            Console.Write("ID: ");
            if (currentField == 0) Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(id.PadRight(1));
            Console.ResetColor();
            Console.WriteLine();

            // Password field
            Console.Write("Password: ");
            if (currentField == 1) Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(new string('*', password.Length).PadRight(1));
            Console.ResetColor();
            Console.WriteLine();

            var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        currentField = (currentField - 1 + 2) % 2;
                        if(!onUser) onUser = true; // Move to ID field if on Password field
                        break;

                    case ConsoleKey.DownArrow:
                        currentField = (currentField + 1) % 2;
                        if (onUser) onUser = false; // Move to Password field if on ID field
                        break;

                    case ConsoleKey.Enter:
                        if (currentField == 0) id = ReadField(id);
                        else if (currentField == 1) password = ReadPassword(password);
                        break;

                    case ConsoleKey.Escape:
                        done = true;
                        break;
                }

                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(password))
                {
                    done = true;
                }
            }
        
        if(ValidateCredentials(id, password))
        {
           IsAuthenticated = true;
            Console.WriteLine("\nValid Credentials. Press Any Key to continue.");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("\nInvalid Credentials. Press Any Key to try Again.");
        Console.ReadKey();
        return;
    }
    //Search Credentials File for matching ID and Password
    public bool ValidateCredentials(string userInput, string pswInput)
    {
        //Open Credentials File
        string[] lines;
        try
        {
            lines = File.ReadAllLines("Credentials.txt");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Credentials File Missing. Please Replace and Try Again");
            return false;
        }
        catch (IOException ex)
        {
            Console.WriteLine($"File error: {ex.Message}");
            return false;
        }

        foreach (string line in lines)
        {
            string[] parts = line.Split('\t');
            if (parts.Length != 3)
            {
                Console.WriteLine("Malformed line in credentials file: " + line);
                continue;
            }
            if(parts[0] == userInput && parts[1] == pswInput)
            {
                id = Convert.ToInt32(parts[0]); // Set the user ID and role upon successful authentication
                role = Convert.ToChar(parts[2]);
                return true;
            }
        }
        return false;
    }
    private static string ReadField(string existing)
    {
        Console.SetCursorPosition(3, Console.CursorTop - 2);
        return Console.ReadLine();
    }
    private static string ReadPassword(string existing)
    {
        Console.SetCursorPosition(10, Console.CursorTop - 1);
        StringBuilder sb = new StringBuilder();
        ConsoleKeyInfo key;
        while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
        {
            if (key.Key == ConsoleKey.Backspace && sb.Length > 0)
            {
                sb.Length--;
                Console.Write("\b \b");
            }
            else if (!char.IsControl(key.KeyChar))
            {
                sb.Append(key.KeyChar);
                Console.Write("*");
            }
        }
        return sb.ToString();
    }
    private void CheckFilesExist()
    {
        Header.Show("Login");
        Header.ResizeWindow(50, 10);
        foreach (var file in requiredFiles)
        {
            try
            {
                if (!File.Exists(file))
                {
                    using (var writer = new StreamWriter(File.Create(file)))
                    {
                        if (file.Equals("credentials.txt", StringComparison.OrdinalIgnoreCase))
                        {
                            writer.WriteLine("10000000\tpassword\ta");
                        }
                        else if (file.Equals("users.txt", StringComparison.OrdinalIgnoreCase))
                        {
                            writer.WriteLine("a\t10000000\tSam Admin\tNull\tNull\t0000000000");
                        }
                        Console.WriteLine($"{file} was missing, created with default admin. Press any key to continue\n");
                        Console.ReadKey();
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Error: You do not have permission to create or access {file}.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"I/O Error while checking or creating {file}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error with {file}: {ex.Message}");
            }
            Console.Clear();
        }
    }
}
