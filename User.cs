using System;
using System.Xml.Linq;

public abstract class User
{
    public int UserID { get; private set; }
    public string fName { get; private set; }
    public int PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public char Role { get; private set; }


    public User(int userId)
    {
        this.UserID = userId;
        LoadUserData();
    }

    public void LoadUserData()
    {
        foreach (var line in File.ReadLines("users.txt"))
        {
            var parts = line.Split(',');
            if (Convert.ToInt32(parts[0]) == this.UserID)
            {
                this.fName = parts[1];
                this.Email = parts[2];
                this.PhoneNumber = Convert.ToInt32(parts[3]);
                this.Email = parts[4];
                this.Address = parts[5];
                Console.WriteLine("User data loaded successfully.");
                break;
            }
        }
    }
}