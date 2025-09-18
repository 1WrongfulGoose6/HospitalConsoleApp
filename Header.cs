using System;

public static class Header
{
    public static void Show(string title)
    {
        Console.Clear();
        Console.WriteLine("┌ " + new string('─', 39) + "┐");
        Console.WriteLine("|" + CenterText("DOTNET Hospital Management System", 40) + "|");
        Console.WriteLine("|" + new string('-', 40) + "|");
        Console.WriteLine("|" + CenterText(title, 40) + "|");
        Console.WriteLine("└" + new string('─', 40) + "┘");
    }
    public static void Appointment()
    {
        Console.WriteLine($"{"Doctor", -15} | {"Patient", -20} | {"Description", -30}");
        Console.WriteLine(new string('-', 70));
    }

    //Example of method overloading
    public static void Appointment(string doctorID, string PatientID, string Description)
    {
        Console.WriteLine($"{doctorID,-15} | {PatientID,-20} | {Description,-30}");
    }
    public static void ResizeWindow(int width, int height)
    {
        Console.SetWindowSize(width, height);
    }
    public static void PrintDoctor(string line)
    {
        string[] parts = line.Split('\t');
        Console.WriteLine($"{parts[2],-20} | {parts[4],-30} | {parts[5],-15} | {parts[3],-40}");
    }
    public static void PrintDoctor()
    {
        Console.WriteLine($"{"Name",-20} | {"Email",-30} | {"Phone",-15} | {"Address",-30}");
        Console.WriteLine(new string('-', 95));
    }
    public static void PrintPatient()
    {
        Console.WriteLine($"{"Patient",-20} | {"Email",-30} | {"Phone",-15} | {"Address",-30}");
        Console.WriteLine(new string('-', 95));
    }
    public static void PrintPatientWithDoctor()
    {
        Console.WriteLine($"{"Patient",-20} | {"Doctor",-20} | {"Email",-30} | {"Phone",-15} | {"Address",-30}");
        Console.WriteLine(new string('-', 95));
    }
    private static string CenterText(string text, int width)
    {
        if (text.Length >= width) return text.Substring(0, width);
        int leftPadding = (width - text.Length) / 2;
        int rightPadding = width - text.Length - leftPadding;
        return new string(' ', leftPadding) + text + new string(' ', rightPadding);
    }
    //public static void PrintPatient(string line)
    //{
    //    string[] parts = line.Split('\t');
    //    Console.WriteLine($"{parts[2],-20} | {parts[4],-30} | {parts[5],-15} | {parts[3],-40}");
    //}
}
