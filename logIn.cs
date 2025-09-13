using System;
using System.IO;
using System.Text;

public class Login
{

    public bool IsAuthenticated { get; internal set; } // Property to track authentication status
    public int id { get; internal set; } // Property to store user ID
    public char role { get; internal set; } // Property to store user role

    // Main authentication method
    public void Authenticate()
    {
        IsAuthenticated = false; // Reset authentication status at the start
        string id = "";
        string password = "";

        int currentField = 0; // 0 = ID, 1 = Password
        bool done = false;


        bool onUser = true; // true = ID field, false = Password field

        // first loop checks fields, second loop validates credentials
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
        
        if(validateCredentials(id, password))
        {
           IsAuthenticated = true;
            Console.WriteLine("\nValid Credentials. Press Any Key to continue.");
            return;
        }
        Console.WriteLine("\nInvalid Credentials. Press Any Key to try Again.");
        Console.ReadKey();
        return;
    }
    
    //Search Credentials File for matching ID and Password
    public bool validateCredentials(string userInput, string pswInput)
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
            string[] parts = line.Split(',');
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

    static string ReadField(string existing)
    {
        Console.SetCursorPosition(3, Console.CursorTop - 2);
        return Console.ReadLine();
    }

    static string ReadPassword(string existing)
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

    internal void AccessUser()
    {
        throw new NotImplementedException();
    }
}
