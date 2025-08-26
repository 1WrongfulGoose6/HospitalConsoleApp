using System;
using System.IO;

public class Login
{
	int userID;
	string password;
    public (int id, char role) Authenticate()
	{
		while (true)
		{
			//Get details and validate
			Console.WriteLine("Welcome to the Login Screen");
			Console.WriteLine("ID: ");
			userID = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Password: ");
			password = Console.ReadLine();

			FileStream newFile = new FileStream("Credentials.txt", FileMode.OpenOrCreate, FileAccess.Read);


			foreach (var line in File.ReadLines("Credentials.txt"))
			{
				Console.WriteLine("returned line is:", line);

				var parts = line.Split(',');
				var user = parts[0];
				var psw = parts[1];
				var role = Convert.ToChar(parts[2]);

				if (Convert.ToInt32(user) == userID && psw == password)
				{
					Console.WriteLine("Login Successful");
					Console.WriteLine("Role: " + role);
					return (userID, role);
				}
			}
			Console.WriteLine("Credentials not found Please Try Again");
		}
    }
}
