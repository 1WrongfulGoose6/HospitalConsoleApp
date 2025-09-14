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

    // Helper method to add padding for centering text
    private static string CenterText(string text, int width)
    {
        if (text.Length >= width) return text.Substring(0, width);
        int leftPadding = (width - text.Length) / 2;
        int rightPadding = width - text.Length - leftPadding;
        return new string(' ', leftPadding) + text + new string(' ', rightPadding);
    }
    internal static void PrintDoctor(string line)
    {
        string[] parts = line.Split('\t');
        Console.WriteLine($"{"Doctor", -20} | {"Email", -30} | {"Address",-30} | {"Phone",-15}");
        Console.WriteLine(new string('-', 95));
        Console.WriteLine($"{parts[2],-20} | {parts[4],-30} | {parts[3],-30} | {parts[5],-15}");
    }

    public static void ResizeWindow(int width, int height)
    {
        Console.SetWindowSize(width, height);
    }

}
